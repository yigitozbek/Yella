﻿using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Yella.Framework.EntityFrameworkCore;
using Yella.Framework.Helper.Results;
using Yella.Framework.Identity.Constants;
using Yella.Framework.Identity.Dtos;
using Yella.Framework.Identity.Entities;
using Yella.Framework.Identity.Helpers.Security.Hashing;
using Yella.Framework.Identity.Helpers.Security.JWT;
using Yella.Framework.Identity.Interfaces;

namespace Yella.Framework.Identity.Services;

public class AuthService<TUser, TRole> : IAuthService<TUser, TRole>
    where TUser : IdentityUser<TUser, TRole>, new()
    where TRole : IdentityRole<TUser, TRole>
{
    private readonly IRepository<UserLoginLog<TUser, TRole>, long> _userLoginLogRepository;
    private readonly IRepository<TUser, Guid> _userRepository;
    private readonly IRepository<UserRole<TUser, TRole>, Guid> _userRoleRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IPermissionService<TUser, TRole> _permissionService;
    private readonly ITokenHelper<TUser, TRole> _tokenHelper;
    private readonly IRoleService<TUser, TRole> _roleService;

    public AuthService(IRepository<TUser, Guid> userRepository,
        IPasswordHasher passwordHasher,
        ITokenHelper<TUser, TRole> tokenHelper,
        IRepository<UserRole<TUser, TRole>, Guid> userRoleRepository,
        IRoleService<TUser, TRole> roleService,
        IPermissionService<TUser, TRole> permissionService,
        IRepository<UserLoginLog<TUser, TRole>, long> userLoginLogRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenHelper = tokenHelper;
        _userRoleRepository = userRoleRepository;
        _roleService = roleService;
        _permissionService = permissionService;
        _userLoginLogRepository = userLoginLogRepository;
        configuration.GetSection("TokenOptions").Get<JwtHelper<TUser, TRole>.TokenOptions>();

    }

    /// <summary>
    /// This method allows it to be registered to the User table.
    /// </summary>
    /// <param name="registerDto"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<IDataResult<TUser>> RegisterAsync(RegisterDto registerDto)
    {

        if (registerDto == null) throw new ArgumentNullException(nameof(registerDto));

        var isUserExit = await _userRepository.FirstOrDefaultAsync(x => x.UserName == registerDto.UserName || x.Email == registerDto.Email);

        if (isUserExit != null)
        {
            return new ErrorDataResult<TUser>("there is a user with a username or email");
        }

        _passwordHasher.CreatePasswordHash(registerDto.Password, out var passwordHash, out var passwordSalt);

        var user = new TUser
        {
            Name = registerDto.Name,
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            Surname = registerDto.Surname,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Joined = DateTime.Now
        };

        var userResult = await _userRepository.AddAsync(user);

        var userRoleList = registerDto.RoleIds.Select(registerDtoRoleId => new UserRole<TUser, TRole>(userResult.Data.Id, registerDtoRoleId)).ToList();

        var result = await _userRoleRepository.AddRangeAsync(userRoleList);

        if (result.Success)
        {
            return new SuccessDataResult<TUser>(userResult.Data, result.Message);
        }

        return new ErrorDataResult<TUser>(result.Message);
    }

    /// <summary>
    /// This method allows it to be login
    /// </summary>
    /// <param name="loginDto"></param>
    /// <param name="claims"></param>
    /// <returns>Return value Token returns</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<IDataResult<AccessToken>> LoginAsync(LoginDto loginDto, List<Claim>? claims = null)
    {

        if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

        var user = await _userRepository.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);

        if (user == null)
        {
            return new ErrorDataResult<AccessToken>(Messages.UserNotFound);
        }

        if (user.IsBlocked)
        {
            return new ErrorDataResult<AccessToken>(Messages.UserBlocked);
        }

        if (!await VerifyPasswordHash(user, loginDto.Password))
        {
            return new ErrorDataResult<AccessToken>(Messages.PasswordError);
        }

        user.IncorrectPasswordAttempts = 0;
        user.LastLogin = DateTime.Now;

        await _userRepository.UpdateAsync(user);

        var accessToken = await CreateToken(user.Id, claims);

        return new SuccessDataResult<AccessToken>(accessToken, Messages.Successful);
    }

    /// <summary>
    /// This method is a Private method. It is used to create tokens.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="claims"></param>
    /// <returns></returns>
    private async Task<AccessToken> CreateToken(Guid userId, List<Claim>? claims)
    {
        var user = await _userRepository.GetAsync(userId);

        var roles = await _roleService.GetListByUserIdAsync(user.Id);

        var permissions = await _permissionService.GetListByUserIdAsync(user.Id);

        var accessToken = _tokenHelper.CreateToken(user, roles, permissions, claims);

        return accessToken;
    }

    /// <summary>
    /// This method is used for resetting the password.
    /// </summary>
    /// <param name="resetPasswordDto"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<IResult> ChangePasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        if (resetPasswordDto == null) throw new ArgumentNullException(nameof(resetPasswordDto));

        var user = await _userRepository.FirstOrDefaultAsync(x => x.Id == resetPasswordDto.Id);

        if (user == null)
        {
            return new ErrorResult("there is no such user");
        }

        if (!_passwordHasher.VerifyPasswordHash(resetPasswordDto.CurrentPassword, user.PasswordHash, user.PasswordSalt))
        {
            return new ErrorResult(Messages.ThisPasswordIsWrong);
        }

        _passwordHasher.CreatePasswordHash(resetPasswordDto.NewPassword, out var passwordHash, out var passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        var result = await _userRepository.UpdateAsync(user);

        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        return new SuccessResult(result.Message);
    }

    /// <summary>
    /// This method checks the correctness of the password.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    private async Task<bool> VerifyPasswordHash(TUser user, string password)
    {
        if (_passwordHasher.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) return true;

        user.IncorrectPasswordAttempts++;

        if (user.IncorrectPasswordAttempts >= 5)
        {
            user.IsBlocked = true;
        }

        await _userRepository.UpdateAsync(user);

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="forgotPasswordDto"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public IDataResult<AccessToken> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
    {

        if (forgotPasswordDto == null) throw new ArgumentNullException(nameof(forgotPasswordDto));

        return new SuccessDataResult<AccessToken>(null, Messages.SuccessfulLogin);
    }

    /// <summary>
    /// This method fetches the last login logs
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<List<UserLoginLog<TUser, TRole>>> GetListUserLoginLogByIdAsync(Guid id)
    {
        var query = await _userLoginLogRepository.GetListAsync(x => x.UserId == id);
        return query.OrderByDescending(x => x.LoginDate).ToList();
    }

    /// <summary>
    /// This method is used to block the user. User cannot login again.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="blockedUser"></param>
    /// <returns></returns>
    public async Task<IDataResult<TUser>> BlockAccountAsync(Guid id, Guid blockedUser)
    {
        if (blockedUser == id)
        {
            return new ErrorDataResult<TUser>("You cannot block your own account.");
        }

        var query = await _userRepository.GetAsync(id);

        query.IsBlocked = true;
        query.BlockedDate = DateTime.Now;
        query.BlockedUserId = blockedUser;

        var result = await _userRepository.UpdateAsync(query);

        if (!result.Success)
        {
            return new ErrorDataResult<TUser>(result.Data, result.Message);
        }

        return new SuccessDataResult<TUser>(result.Data, result.Message);

    }

    /// <summary>
    /// This method is used to activate the blocked user.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="unBlockedUserId"></param>
    /// <returns></returns>
    public async Task<IDataResult<TUser>> UnBlockAccountAsync(Guid id, Guid unBlockedUserId)
    {
        var query = await _userRepository.GetAsync(id);

        query.IsBlocked = false;
        query.BlockedDate = null;
        query.BlockedUserId = null;

        var result = await _userRepository.UpdateAsync(query);

        if (!result.Success)
        {
            return new ErrorDataResult<TUser>(result.Message);
        }

        return new SuccessDataResult<TUser>(result.Data, result.Message);
    }

}