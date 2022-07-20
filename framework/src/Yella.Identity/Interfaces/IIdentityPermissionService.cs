using Yella.Identity.Entities;
using Yella.Identity.Models;
using Yella.Utilities.Results;

namespace Yella.Identity.Interfaces;

public interface IIdentityPermissionService<TUser, TRole>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{
 
    Task<List<Permission<TUser, TRole>>> GetListByUserIdAsync(Guid id);

    Task<List<Permission<TUser, TRole>>> GetListByRoleIdAsync(Guid roleId);

    Task<List<Permission<TUser, TRole>>> GetListAsync();

    Task<IResult> DeleteAsync(short id);

    Task<IDataResult<Permission<TUser, TRole>>> UpdateAsync(Permission<TUser, TRole> input);

    Task<IDataResult<Permission<TUser, TRole>>> AddAsync(Permission<TUser, TRole> input);

}