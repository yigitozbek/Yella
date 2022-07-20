using Yella.EntityFrameworkCore;
using Yella.Identity.Constants;
using Yella.Identity.Entities;
using Yella.Identity.Interfaces;
using Yella.Identity.Models;
using Yella.Utilities.Results;

namespace Yella.Identity.Services;

public class IdentityPermissionService<TUser, TRole> : IIdentityPermissionService<TUser, TRole>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>, new()
{
    private readonly IRepository<TRole, Guid> _roleRepository;
    private readonly IRepository<PermissionRole<TUser, TRole>, Guid> _permissionRoleRepository;
    private readonly IRepository<Permission<TUser, TRole>, short> _permissionRepository;

    public IdentityPermissionService(
        IRepository<Permission<TUser, TRole>, short> permissionRepository,
        IRepository<PermissionRole<TUser, TRole>, Guid> permissionRoleRepository,
        IRepository<TRole, Guid> roleRepository
    )
    {
        _permissionRepository = permissionRepository;
        _permissionRoleRepository = permissionRoleRepository;
        _roleRepository = roleRepository;
    }


    public async Task<List<Permission<TUser, TRole>>> GetListByUserIdAsync(Guid userId)
    {
        var query = (await _permissionRepository.WithIncludeAsync(x => x.PermissionRoles,
                x => ((PermissionRole<TUser, TRole>)x.PermissionRoles).Role,
                x => ((PermissionRole<TUser, TRole>)x.PermissionRoles).Role.UserRoles))
            .Where(x => x.PermissionRoles.Any(pRole => pRole.Role.UserRoles.Any(uRole => uRole.UserId == userId))).ToList();

        return query;
    }

    public async Task<List<Permission<TUser, TRole>>> GetListByRoleIdAsync(Guid roleId)
    {
        var query = (await _permissionRepository.WithIncludeAsync(x => x.PermissionRoles,
                x => ((PermissionRole<TUser, TRole>)x.PermissionRoles).Role))
            .Where(x => x.PermissionRoles.Any(permissionRole => permissionRole.RoleId == roleId)).ToList();

        return query;
    }

    public async Task<List<Permission<TUser, TRole>>> GetListAsync()
    {
        var query = await _permissionRepository.GetListAsync();
        return query.ToList();
    }

    public async Task<IResult> DeleteAsync(short id)
    {
        var result = await _permissionRepository.DeleteAsync(id);

        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        return new SuccessResult(result.Message);
    }

    public async Task<IDataResult<Permission<TUser, TRole>>> UpdateAsync(Permission<TUser, TRole> input)
    {
        var result = await _permissionRepository.UpdateAsync(input);

        if (!result.Success)
        {
            return new ErrorDataResult<Permission<TUser, TRole>>(result.Message);
        }

        return new SuccessDataResult<Permission<TUser, TRole>>(result.Message);
    }

    public async Task<IDataResult<Permission<TUser, TRole>>> AddAsync(Permission<TUser, TRole> input)
    {

        var result = await _permissionRepository.AddAsync(input);

        if (!result.Success)
        {
            return new ErrorDataResult<Permission<TUser, TRole>>(result.Message);
        }

        return new SuccessDataResult<Permission<TUser, TRole>>(result.Message);
    }

}