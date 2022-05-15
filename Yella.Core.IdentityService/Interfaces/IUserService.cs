using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Archseptia.Core.Domain.Results;
using Archseptia.Core.Identity.Domain.Entities;
using Yella.Core.Identity.Domain.Entities;

namespace Yella.Core.IdentityService.Interfaces
{
    public interface IUserService<TUser, TRole>
        where TUser : IdentityUser<TUser, TRole>
        where TRole : IdentityRole<TUser, TRole>
    {
        Task<List<TUser>> GetListAsync();
        Task<List<TUser>> GetListWithRoleAsync();
        Task<TUser> GetWithRoleByIdAsync(Guid id);
        Task<TUser> GetByIdAsync(Guid id, params Expression<Func<TUser, object>>[] includes);
        Task<TUser> GetByUsernameAsync(string username, params Expression<Func<TUser, object>>[] includes);
        Task<DataResult<TUser>> UpdateAsync(TUser input);
        Task<DataResult<TUser>> AddAsync(TUser input);
        Task<string> GetFullNameAsync(Guid id);
    }
}
