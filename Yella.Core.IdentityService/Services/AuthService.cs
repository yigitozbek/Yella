using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Yella.Core.Aspect.Transactions.PostSharp;
using Yella.Core.Aspect.Validations.Postsharp;
using Yella.Core.EntityFrameworkCore;
using Yella.Core.Helper.Results;
using Yella.Core.Identity.Domain.Constants;
using Yella.Core.Identity.Domain.Dtos;
using Yella.Core.Identity.Domain.Entities;
using Yella.Core.Identity.Domain.Validations.FluentValidation;
using Yella.Core.IdentityService.Helpers.Security.Hashing;
using Yella.Core.IdentityService.Helpers.Security.JWT;
using Yella.Core.IdentityService.Interfaces;
using IResult = Yella.Core.Helper.Results.IResult;

namespace Yella.Core.IdentityService.Services;

public class AuthService<TUser, TRole> : IAuthService<TUser, TRole>
    where TUser : IdentityUser<TUser, TRole>, new()
    where TRole : IdentityRole<TUser, TRole>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepository<UserLoginLog<TUser, TRole>, long> _userLoginLogRepository;
    private readonly IRepository<TUser, Guid> _userRepository;
    private readonly IRepository<UserRole<TUser, TRole>, Guid> _userRoleRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IPermissionService<TUser, TRole> _permissionService;
    private readonly ITokenHelper<TUser, TRole> _tokenHelper;
    private readonly IRoleService<TUser, TRole> _roleService;
    private readonly JwtHelper<TUser, TRole>.TokenOptions _tokenOptions;

    public AuthService(IHttpContextAccessor httpContextAccessor,
        IRepository<TUser, Guid> userRepository,
        IPasswordHasher passwordHasher,
        ITokenHelper<TUser, TRole> tokenHelper,
        IRepository<UserRole<TUser, TRole>, Guid> userRoleRepository,
        IRoleService<TUser, TRole> roleService,
        IPermissionService<TUser, TRole> permissionService,
        IRepository<UserLoginLog<TUser, TRole>, long> userLoginLogRepository,
        IConfiguration configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenHelper = tokenHelper;
        _userRoleRepository = userRoleRepository;
        _roleService = roleService;
        _permissionService = permissionService;
        _userLoginLogRepository = userLoginLogRepository;
        _tokenOptions = configuration.GetSection("TokenOptions").Get<JwtHelper<TUser, TRole>.TokenOptions>();

    }

    [ValidateAntiForgeryToken]
    [TransactionAspect(AspectPriority = 2)]
    [FluentValidationAspect(typeof(RegisterValidator), AspectPriority = 1)]
    public async Task<IDataResult<TUser>> RegisterAsync(RegisterDto registerDto)
    {
        var isUserExit = await _userRepository.FirstOrDefaultAsync(x => x.UserName == registerDto.UserName || x.Email == registerDto.Email);

        if (isUserExit != null)
            return new ErrorDataResult<TUser>("there is a user with a username or email");

        _passwordHasher.CreatePasswordHash(registerDto.Password, out var passwordHash, out var passwordSalt);

        var user = new TUser();

        if (registerDto.Image != null)
        {
            var image = "";
            await using var ms = new MemoryStream();
            await registerDto.Image.CopyToAsync(ms);
            var fileBytes = ms.ToArray();
            image = Convert.ToBase64String(fileBytes);
            user.Image = Encoding.ASCII.GetBytes(image);
        }

        user.Name = registerDto.Name;
        user.UserName = registerDto.UserName;
        user.Email = registerDto.Email;
        user.Surname = registerDto.Surname;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        user.Joined = DateTime.Now;

        var role = new UserRole<TUser, TRole>
        {
            User = user,
        };

        var result = await _userRoleRepository.AddAsync(role);

        if (result.Success)
            return new SuccessDataResult<TUser>(null, result.Message);

        return new ErrorDataResult<TUser>(result.Message);
    }

    [ValidateAntiForgeryToken]
    [TransactionAspect(AspectPriority = 2)]
    [FluentValidationAspect(typeof(LoginValidator), AspectPriority = 1)]
    public async Task<IDataResult<AccessToken>> LoginAsync(LoginDto loginDto, List<Claim>? claims = null)
    {
        var user = await _userRepository.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);

        if (user == null)
            return new ErrorDataResult<AccessToken>(Messages.UserNotFound);

        if (user.IsBlocked)
            return new ErrorDataResult<AccessToken>(Messages.UserBlocked);

        if (!await VerifyPasswordHash(user, loginDto.Password))
            return new ErrorDataResult<AccessToken>(Messages.PasswordError);

        await _userRepository.UpdateAsync(user);

        user.IncorrectPasswordAttempts = 0;
        user.LastLogin = DateTime.Now;

        await _userRepository.UpdateAsync(user);

        return new SuccessDataResult<AccessToken>(Messages.Successful);
    }

    [ValidateAntiForgeryToken]
    [TransactionAspect(AspectPriority = 2)]
    [FluentValidationAspect(typeof(ResetPasswordValidator), AspectPriority = 1)]
    public async Task<IDataResult<TUser?>> ChangePasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        var user = await _userRepository.FirstOrDefaultAsync(x => x.Id == resetPasswordDto.Id);

        if (user == null) return new ErrorDataResult<TUser?>(null, "there is no such user");

        if (!_passwordHasher.VerifyPasswordHash(resetPasswordDto.CurrentPassword, user.PasswordHash, user.PasswordSalt))
            return new ErrorDataResult<TUser?>(user, Messages.ThisPasswordIsWrong);

        if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
            return new ErrorDataResult<TUser?>(user, "The password and its confirm are not the same");

        _passwordHasher.CreatePasswordHash(resetPasswordDto.NewPassword, out var passwordHash, out var passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        var result = await _userRepository.UpdateAsync(user);

        if (!result.Success)
            return new ErrorDataResult<TUser?>(user, result.Message);

        return new SuccessDataResult<TUser?>(user, result.Message);
    }

    private async Task<bool> VerifyPasswordHash(TUser user, string password)
    {
        if (_passwordHasher.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) return true;

        user.IncorrectPasswordAttempts++;

        if (user.IncorrectPasswordAttempts >= 5)
            user.IsBlocked = true;

        await _userRepository.UpdateAsync(user);

        return false;

    }

    public async Task<IDataResult<AccessToken>> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
    {
        return new SuccessDataResult<AccessToken>(null, Messages.SuccessfulLogin);
    }

    public async Task<List<UserLoginLog<TUser, TRole>>> GetListUserLoginLogByIdAsync(Guid id)
    {
        var query = await _userLoginLogRepository.GetListAsync(x => x.UserId == id);
        return query.OrderByDescending(x => x.LoginDate).ToList();
    }

    public async Task<IDataResult<TUser?>> BlockAccountAsync(Guid id, Guid blockedUser)
    {
        if (blockedUser == id)
            return new ErrorDataResult<TUser?>("You cannot block your own account.");

        var query = await _userRepository.GetAsync(id);

        query.IsBlocked = true;
        query.BlockedDate = DateTime.Now;
        query.BlockedUserId = blockedUser;

        var result = await _userRepository.UpdateAsync(query);

        if (!result.Success)
            return new ErrorDataResult<TUser?>(result.Message);

        return new SuccessDataResult<TUser?>(null, result.Message);

    }

    public async Task<IDataResult<TUser?>> UnBlockAccountAsync(Guid id, Guid unBlockedUserId)
    {
        var query = await _userRepository.GetAsync(id);

        query.IsBlocked = false;
        query.BlockedDate = null;
        query.BlockedUserId = null;

        var result = await _userRepository.UpdateAsync(query);

        if (!result.Success)
            return new ErrorDataResult<TUser?>(result.Message);

        return new SuccessDataResult<TUser?>(null, result.Message);

    }

    public async Task<IDataResult<UserRole<TUser, TRole>>> AddUserRoleAsync(UserRoleAddDto input)
    {
        var userRoleRoleIds = (await _userRoleRepository.GetListAsync(x => x.UserId == input.UserId)).Select(x => x.RoleId).ToList();
        var roles = input.RoleIds.Where(x => !userRoleRoleIds.Contains(x)).ToList();
        var userRoles = roles.Select(roleId => new UserRole<TUser, TRole>() { UserId = input.UserId, RoleId = roleId }).ToList();

        var result = await _userRoleRepository.AddRangeAsync(userRoles);
        if (!result.Success)
            return new ErrorDataResult<UserRole<TUser, TRole>>(result.Message);

        return new SuccessDataResult<UserRole<TUser, TRole>>(result.Message);
    }

    public async Task<IResult> RemoveUserRoleAsync(UserRoleRemoveDto input)
    {
        var queries =
            await _userRoleRepository.GetListAsync(
                x => input.UserId == x.UserId && input.RoleIds.Contains(x.RoleId));


        foreach (var query in queries)
            await _userRoleRepository.DeleteAsync(query.Id);

        return new SuccessResult();
    }

    public async Task<List<UserLoginLog<TUser, TRole>>> GetListUserLoginLogLastFiveSuccessByIdAsync(Guid id)
    {
        var query = await _userLoginLogRepository.GetListAsync(x => x.UserId == id && x.IsSuccessful);
        return query.Take(5).OrderByDescending(x => x.LoginDate).ToList();
    }

    public async Task<List<UserLoginLog<TUser, TRole>>> GetListUserLoginLogLastFiveErrorByIdAsync(Guid id)
    {
        var query = await _userLoginLogRepository.GetListAsync(x => x.UserId == id && !x.IsSuccessful);
        return query.Take(5).OrderByDescending(x => x.LoginDate).ToList();
    }

    Task<IResult> IAuthService<TUser, TRole>.RemoveUserRoleAsync(UserRoleRemoveDto input)
    {
        throw new NotImplementedException();
    }
}