 
namespace Yella.Framework.Identity.Dtos;

public class PermissionRoleForPermissionListDto  
{
    public PermissionRoleForPermissionListDto(string roleName, string permissionName)
    {
        RoleName = roleName;
        PermissionName = permissionName;
    }

    public PermissionRoleForPermissionListDto()
    {
        
    }

    public string RoleName { get; set; }
    public string PermissionName { get; set; }
}