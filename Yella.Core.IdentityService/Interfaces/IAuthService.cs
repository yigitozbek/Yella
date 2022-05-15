using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Archseptia.Core.Domain.Results;
using Archseptia.Core.Identity.Domain.Entities;
using Archseptia.Core.Identity.Service.Dtos;
using Archseptia.Core.Identity.Service.Helpers.Security.JWT;
using Yella.Core.Identity.Domain.Entities;

namespace Archseptia.Core.Identity.Service.Interfaces
{
    public interface IAuthService<TUser, TRole> 
        where TUser : IdentityUser<TUser, TRole>
        where TRole : IdentityRole<TUser, TRole>
    {
        Task<IDataResult<TUser>> RegisterAsync(RegisterDto registerDto);
        Task<IDataResult<AccessToken>> LoginAsync(LoginDto loginDto, List<Claim> claims);
        Task<List<UserLoginLog<TUser, TRole>>> GetListUserLoginLogByIdAsync(Guid id);
        Task<List<UserLoginLog<TUser, TRole>>> GetListUserLoginLogLastFiveSuccessByIdAsync(Guid id);
        Task<List<UserLoginLog<TUser, TRole>>> GetListUserLoginLogLastFiveErrorByIdAsync(Guid id);
        Task<IDataResult<TUser?>> ChangePasswordAsync(ResetPasswordDto resetPasswordDto);
        Task<IDataResult<TUser?>> BlockAccountAsync(Guid id, Guid blockedUser);
        Task<IDataResult<UserRole<TUser, TRole>>> AddUserRoleAsync(UserRoleAddDto input);
        Task<IResult> RemoveUserRoleAsync(UserRoleRemoveDto input);
        Task<IDataResult<TUser?>> UnBlockAccountAsync(Guid id, Guid unBlockedUserId);
    }
}
