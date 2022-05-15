using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Archseptia.Core.Domain.Results;
using Archseptia.Core.Identity.Domain.Entities;
using Archseptia.Core.Identity.Service.Dtos;
using Yella.Core.Identity.Domain.Entities;

namespace Archseptia.Core.Identity.Service.Interfaces
{
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
}