using Microsoft.AspNetCore.Builder;

namespace Yella.Core.IdentityService.Middlewares
{
    public static class IdentityMiddleware
    {
        public static IApplicationBuilder UseIdentity(this IApplicationBuilder builder) => builder.UseMiddleware<AuthenticationMiddleware>();
    }
}
