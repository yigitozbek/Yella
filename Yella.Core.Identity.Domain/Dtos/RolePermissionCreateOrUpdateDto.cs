using System.ComponentModel.DataAnnotations;
using Yella.Core.Domain.Dto;

namespace Yella.Core.Identity.Dtos;

public class RolePermissionCreateOrUpdateDto : EntityDto<Guid>
{
    protected RolePermissionCreateOrUpdateDto(string roleName, string description, PermissionDto[] permissions)
    {
        RoleName = roleName;
        Description = description;
        Permissions = permissions;
    }

    protected RolePermissionCreateOrUpdateDto(Guid id, string roleName, string description, PermissionDto[] permissions) : base(id)
    {
        RoleName = roleName;
        Description = description;
        Permissions = permissions;
    }

    public string RoleName { get; set; }

    public string Description { get; set; }
    public PermissionDto[] Permissions { get; set; }
}