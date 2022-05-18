using Yella.Core.Aspect.Transactions.PostSharp;
using Yella.Core.EntityFrameworkCore;
using Yella.Core.Helper.Results;
using Yella.Core.Identity.Domain.Constants;
using Yella.Core.Identity.Domain.Dtos;
using Yella.Core.Identity.Domain.Entities;
using Yella.Core.IdentityService.Interfaces;

namespace Yella.Core.IdentityService.Services;

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
        var query = (await _permissionRoleRepository
                .WithDetailsAsync(x => x.Permission, x => x.Role))
            .Select(x => new PermissionRoleForPermissionListDto()
            {
                PermissionName = x.Permission.Tag,
                RoleName = x.Role.Name
            }).ToList();

        var c = query
            .GroupBy(x => new PermissionRoleForPermissionListDto
            {
                PermissionName = x.PermissionName,
                RoleName = x.RoleName
            }).ToList();

        //TODO: Düzeltilecek

        return c.Select(VARIABLE => new PermissionRoleForPermissionListDto() { RoleName = VARIABLE.ToList().First().RoleName, PermissionName = VARIABLE.ToList().First().PermissionName }).ToList();
    }


    [TransactionAspect]
    public async Task<List<Permission<TUser, TRole>>> GetListByUserIdAsync(Guid userId)
    {
        var result = (await _permissionRepository.WithDetailsAsync(x => x.PermissionRoles,
                x => ((PermissionRole<TUser, TRole>)x.PermissionRoles).Role,
                x => ((PermissionRole<TUser, TRole>)x.PermissionRoles).Role.UserRoles))
            .Where(x => x.PermissionRoles.Any(pRole => pRole.Role.UserRoles.Any(uRole => uRole.UserId == userId))).ToList();

        return result;
    }

    public async Task<List<Permission<TUser, TRole>>> GetListByRoleIdAsync(Guid roleId)
    {
        var result = (await _permissionRepository.WithDetailsAsync(x => x.PermissionRoles,
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

    [TransactionAspect]
    public async Task<Result> RolePermissionCreateOrUpdateAsync(RolePermissionCreateOrUpdateDto input)
    {
        return input.Id != Guid.Empty
            ? await UpdateAsync(input)
            : await CreateAsync(input);
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var result = await _roleRepository.DeleteAsync(id);

        if (!result.Success)
            return new ErrorResult(result.Message);

        return new SuccessResult(result.Message);
    }

    [TransactionAspect]
    private async Task<Result> UpdateAsync(RolePermissionCreateOrUpdateDto input)
    {
        //Todo: Düzeltilecek

        var role = await _roleRepository.GetAsync(input.Id);
        role.Name = input.RoleName;
        role.Description = input.Description;
        await _roleRepository.UpdateAsync(role);
        var query = await _permissionRoleRepository.GetListAsync(x => x.RoleId == input.Id);
        foreach (var permissionRole in query) await _permissionRoleRepository.DeleteAsync(permissionRole);
        var list = input.Permissions.Where(x => x.IsChecked).ToList().Select(permissionDto => new PermissionRole<TUser, TRole>() { RoleId = input.Id, PermissionId = permissionDto.Id }).ToList();
        await _permissionRoleRepository.AddRangeAsync(list);
        return new SuccessResult(Messages.Updated);
    }

    [TransactionAspect]
    private async Task<Result> CreateAsync(RolePermissionCreateOrUpdateDto input)
    {
        var role = new TRole { Name = input.RoleName, Description = input.Description };
        var result = await _roleRepository.AddAsync(role);

        var list = input.Permissions.Where(x => x.IsChecked).Select(permissionDto =>
        {
            var permissionRole = new PermissionRole<TUser, TRole>();
            permissionRole.RoleId = result.Data.Id;
            permissionRole.PermissionId = permissionDto.Id;
            return permissionRole;
        }).ToList();

        await _permissionRoleRepository.AddRangeAsync(list);
        return new SuccessResult(Messages.Updated);
    }



}