using System.Security.Claims;
using Yella.Identity.Entities;

namespace Yella.Identity.Helpers.Security.JWT;

public interface ITokenHelper<TUser, TRole>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{
    AccessToken CreateToken(TUser user, IEnumerable<TRole> roles, IEnumerable<Permission<TUser, TRole>> permissions,List<Claim>? claims);

}