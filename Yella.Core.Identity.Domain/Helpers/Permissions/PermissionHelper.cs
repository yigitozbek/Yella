using System.Security.Claims;

namespace Yella.Core.Identity.Helpers.Permissions;

public static class PermissionHelper
{
    /// <summary>
    /// Checks if the User has such a Role.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="permission"></param>
    /// <returns></returns>
    public static bool CheckPermission(this ClaimsPrincipal user, string permission)
    {
        var permissions = user.Claims
            .Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/permission")
            .Select(x => x.Value).ToList();

        return permissions.Count(x => x == permission) > 0;
    }
}