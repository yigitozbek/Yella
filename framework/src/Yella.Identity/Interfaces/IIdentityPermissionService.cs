using Yella.Identity.Dtos;
using Yella.Identity.Entities;
using Yella.Utilities.Results;

namespace Yella.Identity.Interfaces;

public interface IIdentityPermissionService<TUser, TRole>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{
    Task<List<PermissionRoleForPermissionListDto>> GetListPermissionRoleForPermissionListAsync();

    Task<List<Permission<TUser, TRole>>> GetListByUserIdAsync(Guid id);

    Task<List<Permission<TUser, TRole>>> GetListByRoleIdAsync(Guid roleId);

    Task<List<Permission<TUser, TRole>>> GetListAsync();

    Task<IResult> DeleteAsync(Guid id);

    Task<IDataResult<Permission<TUser, TRole>>> UpdateRangeAsync(RolePermissionCreateOrUpdateDto input);

    Task<IDataResult<Permission<TUser, TRole>>> AddAsync(RolePermissionCreateOrUpdateDto input);

}