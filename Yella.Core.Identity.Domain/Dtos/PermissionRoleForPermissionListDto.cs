using Yella.Core.Domain.Dto;

namespace Yella.Core.Identity.Dtos;

public class PermissionRoleForPermissionListDto : EntityDto
{
    public PermissionRoleForPermissionListDto(string roleName, string permissionName)
    {
        RoleName = roleName;
        PermissionName = permissionName;
    }

    public string RoleName { get; set; }
    public string PermissionName { get; set; }
}