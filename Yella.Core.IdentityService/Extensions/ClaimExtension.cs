using System.Security.Claims;
using Yella.Core.IdentityService.Helpers.Security.Claims;

namespace Yella.Core.IdentityService.Extensions
{
    public static class ClaimExtension
    {
        public static void AddEmail(this ICollection<Claim> claims, string email) => claims.Add(new Claim(CoreClaimTypes.Email, email));
        
        public static void AddName(this ICollection<Claim> claims, string name) => claims.Add(new Claim(CoreClaimTypes.Name, name));
        
        public static void AddUsername(this ICollection<Claim> claims, string username) => claims.Add(new Claim(CoreClaimTypes.Username, username));
        
        public static void AddFullName(this ICollection<Claim> claims, string fullName) => claims.Add(new Claim(CoreClaimTypes.FullName, fullName));
        
        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier) => claims.Add(new Claim(CoreClaimTypes.NameIdentifier, nameIdentifier));
        
        public static void AddAvatar(this ICollection<Claim> claims, string imagePath) => claims.Add(new Claim(CoreClaimTypes.Avatar, imagePath));
        
        public static void AddRoles(this ICollection<Claim> claims, string[] roles) => roles.ToList().ForEach(role => claims.Add(new Claim(CoreClaimTypes.Role, role)));
        
        public static void AddPermissions(this ICollection<Claim> claims, string[] permissions) => permissions.ToList().ForEach(permission => claims.Add(new Claim(CoreClaimTypes.Permission, permission)));

    }
}
