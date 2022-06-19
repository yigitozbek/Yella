using System.Security.Claims;
using Yella.Framework.Helper.Results;
using Yella.Framework.Identity.Dtos;
using Yella.Framework.Identity.Entities;
using Yella.Framework.Identity.Helpers.Security.JWT;

namespace Yella.Framework.Identity.Interfaces;

public interface IAuthService<TUser, TRole> 
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{

    /// <summary>
    /// This method allows it to be registered to the User table.
    /// </summary>
    /// <param name="registerDto"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    Task<IDataResult<TUser>> RegisterAsync(RegisterDto registerDto);

    /// <summary>
    /// This method allows it to be login
    /// </summary>
    /// <param name="loginDto"></param>
    /// <param name="claims"></param>
    /// <returns>Return value Token returns</returns>
    /// <exception cref="ArgumentNullException"></exception>
    Task<IDataResult<AccessToken>> LoginAsync(LoginDto loginDto, List<Claim> claims);

    /// <summary>
    /// This method fetches the last login logs
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<List<UserLoginLog<TUser, TRole>>> GetListUserLoginLogByIdAsync(Guid id);

    /// <summary>
    /// This method is used for resetting the password.
    /// </summary>
    /// <param name="resetPasswordDto"></param>
    /// <exception cref="ArgumentNullException"></exception>
    Task<IResult> ChangePasswordAsync(ResetPasswordDto resetPasswordDto);

    /// <summary>
    /// This method is used to block the user. User cannot login again.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="blockedUser"></param>
    /// <returns></returns>
    Task<IDataResult<TUser>> BlockAccountAsync(Guid id, Guid blockedUser);

    /// <summary>
    /// This method is used to activate the blocked user.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="unBlockedUserId"></param>
    /// <returns></returns>
    Task<IDataResult<TUser>> UnBlockAccountAsync(Guid id, Guid unBlockedUserId);
}