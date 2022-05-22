using Yella.Core.Helper.Results;
using Yella.Core.Identity.Dtos;
using Yella.Core.Identity.Entities;

namespace Yella.Core.Identity.Interfaces;

public interface IPermissionService<TUser, TRole>
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