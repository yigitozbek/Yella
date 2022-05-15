using Microsoft.AspNetCore.Builder;

namespace Archseptia.Core.Identity.Service.Middlewares
{
    public static class IdentityMiddleware
    {
        public static IApplicationBuilder UseIdentity(this IApplicationBuilder builder) => builder.UseMiddleware<AuthenticationMiddleware>();
    }
}
