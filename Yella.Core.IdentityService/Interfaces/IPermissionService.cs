using Yella.Core.Helper.Results;
using Yella.Core.Identity.Domain.Dtos;
using Yella.Core.Identity.Domain.Entities;

namespace Yella.Core.IdentityService.Interfaces;

public interface IPermissionService<TUser, TRole> 
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{
    Task<List<PermissionRoleForPermissionListDto>> GetListPermissionRoleForPermissionListAsync();
    Task<List<Permission<TUser, TRole>>> GetListByUserIdAsync(Guid id);
    Task<List<Permission<TUser, TRole>>> GetListByRoleIdAsync(Guid roleId);
    Task<List<Permission<TUser, TRole>>> GetListAsync();
    Task<Result> RolePermissionCreateOrUpdateAsync(RolePermissionCreateOrUpdateDto input);
    Task<Result> DeleteAsync(Guid id);


}