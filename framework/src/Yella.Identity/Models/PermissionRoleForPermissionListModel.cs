 
namespace Yella.Identity.Models;

public class PermissionRoleForPermissionListModel  
{
    public PermissionRoleForPermissionListModel(string roleName, string permissionName)
    {
        RoleName = roleName;
        PermissionName = permissionName;
    }

    public PermissionRoleForPermissionListModel()
    {
        
    }

    public string RoleName { get; set; }
    public string PermissionName { get; set; }
}