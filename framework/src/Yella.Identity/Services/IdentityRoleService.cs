using Yella.EntityFrameworkCore;
using Yella.Identity.Constants;
using Yella.Identity.Entities;
using Yella.Identity.Interfaces;
using Yella.Identity.Models;
using Yella.Utilities.Results;

namespace Yella.Identity.Services;

public class IdentityRoleService<TUser, TRole> : IIdentityRoleService<TUser, TRole>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>, new()
{
    private readonly IRepository<TRole, Guid> _roleRepository;
    private readonly IRepository<UserRole<TUser, TRole>, Guid> _userRoleRepository;
    private readonly IRepository<PermissionRole<TUser, TRole>, Guid> _permissionRoleRepository;

    public IdentityRoleService(IRepository<TRole, Guid> roleRepository,
        IRepository<UserRole<TUser, TRole>, Guid> userRoleRepository,
        IRepository<PermissionRole<TUser, TRole>, Guid> permissionRoleRepository)
    {
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _permissionRoleRepository = permissionRoleRepository;
    }

    /// <summary>
    /// This method fetches roles by user Id.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<TRole>> GetListByUserIdAsync(Guid userId)
    {

        var result = (await _roleRepository.WithIncludeAsync(x => x.UserRoles,
                x => ((UserRole<TUser, TRole>)x.UserRoles).Role!,
                x => ((UserRole<TUser, TRole>)x.UserRoles).Role!.UserRoles))
            .Where(x => x.UserRoles.Any(pRole => pRole.Role!.UserRoles.Any(uRole => uRole.UserId == userId))).ToList();

        return result;
    }

    /// <summary>
    /// This method returns roles that the user does not have.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<TRole>> GetListWithoutRoleByUserIdAsync(Guid userId)
    {

        var userRoleRoleIds = (await _userRoleRepository.GetListAsync(x => x.UserId == userId)).Select(x => x.RoleId).ToList();

        var roles = (await _roleRepository.GetListAsync()).Where(x => !userRoleRoleIds.Contains(x.Id)).ToList();

        return roles;
    }

    /// <summary>
    /// This method fetches the roles
    /// </summary>
    /// <returns></returns>
    public async Task<List<TRole>> GetListAsync()
    {
        var query = await _roleRepository.GetListAsync();
        return query.ToList();
    }

    /// <summary>
    /// This method fetches the role by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<TRole> GetByIdAsync(Guid id)
    {
        var query = await _roleRepository.GetAsync(id);
        return query;
    }

    /// <summary>
    /// This method is used to add Role
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<IDataResult<TRole>> AddAsync(TRole input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));

        var result = await _roleRepository.AddAsync(input);

        if (!result.Success)
        {
            return new ErrorDataResult<TRole>(result.Data, result.Message);
        }

        return new SuccessDataResult<TRole>(result.Data, result.Message);
    }

    /// <summary>
    /// After this method adds the role, it also adds the permissions that depend on the role. 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<IDataResult<TRole>> AddWithPermissionAsync(RolePermissionModel input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));

        var roleResult = await _roleRepository.AddAsync(new TRole() { Name = input.Name, Description = input.Description, Code = input.Code });

        if (!roleResult.Success)
        {
            return new ErrorDataResult<TRole>(roleResult.Data, roleResult.Message);
        }

        var permissions = input.Permissions.Where(x => x.IsChecked).Select(permissionDto => new PermissionRole<TUser, TRole> { RoleId = roleResult.Data.Id, PermissionId = permissionDto.Id }).ToList();

        var permissionResult = await _permissionRoleRepository.AddRangeAsync(permissions);

        if (!permissionResult.Success)
        {
            return new ErrorDataResult<TRole>(roleResult.Data, permissionResult.Message);
        }

        return new SuccessDataResult<TRole>(roleResult.Data, permissionResult.Message);
    }


    /// <summary>
    /// After this method update the role, it also updates the permissions that depend on the role. 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<IDataResult<TRole>> UpdateWithPermissionAsync(RolePermissionModel input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));

        var roleResult = await _roleRepository.AddAsync(new TRole() { Name = input.Name, Description = input.Description, Code = input.Code });

        if (!roleResult.Success)
        {
            return new ErrorDataResult<TRole>(roleResult.Data, roleResult.Message);
        }

        var permissionDeleteResult = await _permissionRoleRepository.DeleteAsync(x => x.RoleId == input.Id);

        if (!permissionDeleteResult.Success)
        {
            return new ErrorDataResult<TRole>(roleResult.Data, permissionDeleteResult.Message);
        }

        var permissions = input.Permissions.Where(x => x.IsChecked).Select(permissionDto => new PermissionRole<TUser, TRole> { RoleId = roleResult.Data.Id, PermissionId = permissionDto.Id }).ToList();

        var permissionResult = await _permissionRoleRepository.AddRangeAsync(permissions);

        if (!permissionResult.Success)
        {
            return new ErrorDataResult<TRole>(roleResult.Data, permissionResult.Message);
        }

        return new SuccessDataResult<TRole>(roleResult.Data, permissionResult.Message);
    }

    /// <summary>
    /// This method is used to update Role
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<IDataResult<TRole>> UpdateAsync(TRole input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));

        var result = await _roleRepository.UpdateAsync(input);

        if (!result.Success)
        {
            return new ErrorDataResult<TRole>(result.Data, result.Message);
        }

        return new SuccessDataResult<TRole>(result.Data, result.Message);
    }


    /// <summary>
    /// This method is used to add Role to User.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<IDataResult<UserRole<TUser, TRole>>> AddUserRoleAsync(CreateUserRoleModel input)
    {

        if (input == null) throw new ArgumentNullException(nameof(input));

        var userRoleRoleIds = (await _userRoleRepository.GetListAsync(x => x.UserId == input.UserId)).Select(x => x.RoleId);

        var roles = input.RoleIds.Where(x => !userRoleRoleIds.Contains(x)).ToList();

        var userRoles = roles.Select(roleId => new UserRole<TUser, TRole>(input.UserId, roleId)).ToList();

        var result = await _userRoleRepository.AddRangeAsync(userRoles);

        if (!result.Success)
        {
            return new ErrorDataResult<UserRole<TUser, TRole>>(result.Message);
        }

        return new SuccessDataResult<UserRole<TUser, TRole>>(result.Message);
    }

    /// <summary>
    /// This method is used to remove Role to User.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<IResult> RemoveUserRoleAsync(UserRoleRemoveModel input)
    {

        if (input == null) throw new ArgumentNullException(nameof(input));

        var queries = await _userRoleRepository.GetListAsync(x => input.UserId == x.UserId && input.RoleIds.Contains(x.RoleId));

        foreach (var query in queries)
        {
            await _userRoleRepository.DeleteAsync(query.Id);
        }

        return new SuccessResult(IdentityMessages.Removed);
    }
}