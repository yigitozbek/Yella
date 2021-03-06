using System.ComponentModel.DataAnnotations.Schema;
using Yella.Domain.Entities;

namespace Yella.Identity.Entities;

public class PermissionRole<TUser, TRole> : FullAuditedEntity<Guid>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{
    public PermissionRole(Guid roleId, short permissionId)
    {
        PermissionId = permissionId;
        RoleId = roleId;
    }

    public PermissionRole(Permission<TUser, TRole> permission, short permissionId, TRole role, Guid roleId)
    {
        Permission = permission;
        PermissionId = permissionId;
        Role = role;
        RoleId = roleId;
    }

    public PermissionRole()
    {

    }




    [ForeignKey(nameof(PermissionId))]
    public virtual Permission<TUser, TRole> Permission { get; set; }
    public short PermissionId { get; set; }


    [ForeignKey(nameof(RoleId))]
    public virtual TRole Role { get; set; }
    public Guid RoleId { get; set; }
}