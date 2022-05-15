using Microsoft.AspNetCore.Http;
using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;
using PostSharp.Serialization;
using System;
using System.Linq;
using System.Security.Authentication;
using System.Text.Json;
using Yella.Core.Domain.Results;

namespace Yella.Core.Aspect.Authorizations.PostSharp
{
    [PSerializable]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor)]
    [ProvideAspectRole(StandardRoles.Security)]
    [AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.Before, StandardRoles.Tracing)]
    public class AuthorizationAspect : OnMethodBoundaryAspect
    {
        public string? Permission = null;

        public AuthorizationAspect(string? permission = null) => Permission = permission;

        public override void OnEntry(MethodExecutionArgs args)
        {
            HttpContext? httpContext = args.Instance as HttpContext;

            if (httpContext == null || httpContext.User.Identity is { IsAuthenticated: false })
                throw new AuthenticationException();

            var permissions = httpContext.User.Claims
                .Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/permission")
                .Select(x => x.Value).ToList();

            if (!string.IsNullOrEmpty(Permission) && permissions.Count(x => x == Permission) == 0)
                throw new UnauthorizedAccessException(JsonSerializer.Serialize(new Result(false, "You are not authorized")));

        }
    }
}
