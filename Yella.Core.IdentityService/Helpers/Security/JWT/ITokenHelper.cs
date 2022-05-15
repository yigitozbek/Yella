using System.Collections.Generic;
using System.Security.Claims;
using Archseptia.Core.Identity.Domain.Entities;
using Yella.Core.Identity.Domain.Entities;

namespace Archseptia.Core.Identity.Service.Helpers.Security.JWT
{
    public interface ITokenHelper<TUser, TRole>
        where TUser : IdentityUser<TUser, TRole>
        where TRole : IdentityRole<TUser, TRole>
    {
        AccessToken CreateToken(TUser user, IEnumerable<TRole> roles, IEnumerable<Permission<TUser, TRole>> permissions,List<Claim> claims);

    }
}