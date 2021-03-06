using System.Linq.Expressions;
using Yella.Identity.Entities;
using Yella.Utilities.Results;

namespace Yella.Identity.Interfaces;

public interface IIdentityUserService<TUser, TRole>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{
    Task<List<TUser>> GetListAsync();
    Task<List<TUser>> GetListWithRoleAsync();
    Task<TUser> GetWithRoleByIdAsync(Guid id);
    Task<TUser> GetByIdAsync(Guid id, params Expression<Func<TUser, object>>[]? includes);
    Task<TUser?> GetByUsernameAsync(string username, params Expression<Func<TUser, object>>[] includes);
    Task<IDataResult<TUser>> UpdateAsync(TUser input);
    Task<IDataResult<TUser>> AddAsync(TUser input);
    Task<string?> GetFullNameAsync(Guid id);
}