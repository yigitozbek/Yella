using Yella.Core.Aspect.Transactions.PostSharp;
using Yella.Core.EntityFrameworkCore;
using Yella.Core.Helper.Results;
using Yella.Core.Identity.Constants;
using Yella.Core.Identity.Dtos;
using Yella.Core.Identity.Entities;
using Yella.Core.Identity.Interfaces;

namespace Yella.Core.Identity.Services;

public class PermissionService<TUser, TRole> : IPermissionService<TUser, TRole>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>, new()
{
    private readonly IRepository<TRole, Guid> _roleRepository;
    private readonly IRepository<PermissionRole<TUser, TRole>, Guid> _permissionRoleRepository;
    private readonly IRepository<Permission<TUser, TRole>, short> _permissionRepository;

    public PermissionService(
        IRepository<Permission<TUser, TRole>, short> permissionRepository,
        IRepository<PermissionRole<TUser, TRole>, Guid> permissionRoleRepository,
        IRepository<TRole, Guid> roleRepository
    )
    {
        _permissionRepository = permissionRepository;
        _permissionRoleRepository = permissionRoleRepository;
        _roleRepository = roleRepository;
    }

    public async Task<List<PermissionRoleForPermissionListDto>> GetListPermissionRoleForPermissionListAsync()
    {
        var query = (await _permissionRoleRepository.WithIncludeAsync(x => x.Permission, x => x.Role))
            .Select(x => new PermissionRoleForPermissionListDto(permissionName: x.Permission.Tag, roleName: x.Role.Name)).ToList();

        var c = query
            .GroupBy(x => new PermissionRoleForPermissionListDto(permissionName: x.PermissionName, roleName: x.RoleName)).ToList();

        //TODO: Düzeltilecek

        return c.Select(variable => new PermissionRoleForPermissionListDto(variable.ToList().First().RoleName,
            variable.ToList().First().PermissionName)).ToList();
    }


    public async Task<List<Permission<TUser, TRole>>> GetListByUserIdAsync(Guid userId)
    {
        var result = (await _permissionRepository.WithIncludeAsync(x => x.PermissionRoles,
                x => ((PermissionRole<TUser, TRole>)x.PermissionRoles).Role,
                x => ((PermissionRole<TUser, TRole>)x.PermissionRoles).Role.UserRoles))
            .Where(x => x.PermissionRoles.Any(pRole => pRole.Role.UserRoles.Any(uRole => uRole.UserId == userId))).ToList();

        return result;
    }

    public async Task<List<Permission<TUser, TRole>>> GetListByRoleIdAsync(Guid roleId)
    {
        var result = (await _permissionRepository.WithIncludeAsync(x => x.PermissionRoles,
                x => ((PermissionRole<TUser, TRole>)x.PermissionRoles).Role,
                x => ((PermissionRole<TUser, TRole>)x.PermissionRoles).Role.UserRoles))
            .Where(x => x.PermissionRoles.Any(x => x.RoleId == roleId)).ToList();

        return result;
    }

    public async Task<List<Permission<TUser, TRole>>> GetListAsync()
    {
        var result = await _permissionRepository.GetListAsync();
        return result.ToList();
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var result = await _roleRepository.DeleteAsync(id);

        if (!result.Success)
            return new ErrorResult(result.Message);

        return new SuccessResult(result.Message);
    }

    [TransactionAspect]
    public async Task<IDataResult<Permission<TUser, TRole>>> UpdateRangeAsync(RolePermissionCreateOrUpdateDto input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));

        var role = await _roleRepository.GetAsync(input.Id);

        role.Name = input.RoleName;
        role.Description = input.Description;

        var roleResult = await _roleRepository.UpdateAsync(role);

        if (!roleResult.Success)
        {
            return new ErrorDataResult<Permission<TUser, TRole>>(roleResult.Message);
        }

        var query = await _permissionRoleRepository.GetListAsync(x => x.RoleId == input.Id);

        foreach (var permissionRole in query)
        {
            await _permissionRoleRepository.DeleteAsync(permissionRole);
        }

        var list = input.Permissions.Where(x => x.IsChecked).ToList().Select(permissionDto => new PermissionRole<TUser, TRole>(input.Id, permissionDto.Id)).ToList();

        var results = await _permissionRoleRepository.AddRangeAsync(list);

        return new SuccessDataResult<Permission<TUser, TRole>>(results.Message);
    }

    public async Task<IDataResult<Permission<TUser, TRole>>> AddAsync(RolePermissionCreateOrUpdateDto input)
    {

        if (input == null) throw new ArgumentNullException(nameof(input));

        var role = new TRole { Name = input.RoleName, Description = input.Description };

        var result = await _roleRepository.AddAsync(role);

        if (!result.Success)
        {
            return new ErrorDataResult<Permission<TUser, TRole>>(result.Message);
        }

        PermissionRole<TUser, TRole> Selector(PermissionDto permissionDto)
        {
            var permissionRole = new PermissionRole<TUser, TRole>(result.Data.Id, permissionDto.Id);
            return permissionRole;
        }

        var list = input.Permissions.Where(x => x.IsChecked).Select(Selector).ToList();

        var results = await _permissionRoleRepository.AddRangeAsync(list);

        return new SuccessDataResult<Permission<TUser, TRole>>(results.Message);
    }



}