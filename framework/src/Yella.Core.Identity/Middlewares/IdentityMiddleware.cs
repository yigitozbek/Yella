using Microsoft.AspNetCore.Builder;

namespace Yella.Framework.Identity.Middlewares;

public static class IdentityMiddleware
{
    public static IApplicationBuilder UseIdentity(this IApplicationBuilder builder) => builder.UseMiddleware<AuthenticationMiddleware>();
}