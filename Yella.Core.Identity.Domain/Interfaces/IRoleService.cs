using Yella.Core.Identity.Entities;

namespace Yella.Core.Identity.Interfaces;

public interface IRoleService<TUser, TRole>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{
    Task<List<TRole>> GetListByUserIdAsync(Guid id);
    Task<List<TRole>> GetListWithoutRoleByUserIdAsync(Guid id);
    Task<List<TRole>> GetListAsync();
    Task<TRole> GetByIdAsync(Guid id);
}