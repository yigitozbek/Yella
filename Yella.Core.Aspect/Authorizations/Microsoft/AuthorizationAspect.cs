using System;
using System.Linq;
using System.Security.Authentication;
using System.Text.Json;

namespace Archseptia.Core.Aspect.Authorization.Microsoft
{
    public class AuthorizationAspect : Attribute//, IAuthorizationFilter
    {
        public string? Permission = null;
        public int AspectPriority { get; set; }

        public AuthorizationAspect(string? permission = null)
        {
            Permission = permission;
        }


        //public void OnAuthorization(AuthorizationFilterContext context)
        //{

        //    //Worker Job dropdown a bağlanacak
        //    var httpContext = context.HttpContext;

        //    if (httpContext == null || httpContext.User.Identity is { IsAuthenticated: false })
        //        throw new AuthenticationException();

        //    var permissions = httpContext.User.Claims
        //        .Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/permission")
        //        .Select(x => x.Value).ToList();

        //    if (!string.IsNullOrEmpty(Permission) && permissions.Count(x => x == Permission) == 0)
        //        throw new UnauthorizedAccessException(JsonSerializer.Serialize(new Result(false, "You are not authorized")));
        //}





    }
}
