using Microsoft.AspNetCore.Builder;

namespace Yella.Core.Identity.Middlewares;

public static class IdentityMiddleware
{
    public static IApplicationBuilder UseIdentity(this IApplicationBuilder builder) => builder.UseMiddleware<AuthenticationMiddleware>();
}