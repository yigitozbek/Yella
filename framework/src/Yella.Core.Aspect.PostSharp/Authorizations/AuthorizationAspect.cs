using System.Security.Authentication;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;
using PostSharp.Serialization;
using Yella.Framework.Utilities.Extensions;
using Yella.Framework.Utilities.Results;

namespace Yella.Framework.Aspect.PostSharp.Authorizations;

[PSerializable]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Class)]
[ProvideAspectRole(StandardRoles.Security)]
public class AuthorizationAspect : OnMethodBoundaryAspect
{

    public string? Permission = null;

    public AuthorizationAspect(string? permission = null)
    {
        Permission = permission;
    }

    public override void OnEntry(MethodExecutionArgs args)
    {
        var httpContext =
            ((HttpContextAccessor)(ServiceActivator.GetScope()?.ServiceProvider
                .GetService(typeof(IHttpContextAccessor)))!)?.HttpContext;

        if (httpContext == null || httpContext.User.Identity is { IsAuthenticated: false })
            throw new AuthenticationException();

        var permissions = httpContext.User.Claims
            .Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/permission")
            .Select(x => x.Value).ToList();

        if (!string.IsNullOrEmpty(Permission) && permissions.Count(x => x == Permission) == 0)
            throw new UnauthorizedAccessException(JsonSerializer.Serialize(new ErrorResult("You are not authorized")));


    }


}