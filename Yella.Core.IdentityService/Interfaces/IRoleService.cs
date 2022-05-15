using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Archseptia.Core.Identity.Domain.Entities;
using Yella.Core.Identity.Domain.Entities;

namespace Yella.Core.IdentityService.Interfaces
{
    public interface IRoleService<TUser, TRole>
        where TUser : IdentityUser<TUser, TRole>
        where TRole : IdentityRole<TUser, TRole>
    {
        Task<List<TRole>> GetListByUserIdAsync(Guid id);
        Task<List<TRole>> GetListWithoutRoleByUserIdAsync(Guid id);
        Task<List<TRole>> GetListAsync();
        Task<TRole> GetByIdAsync(Guid id);
    }
}