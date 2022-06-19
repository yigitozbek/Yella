using System.ComponentModel.DataAnnotations;
using Yella.Framework.Domain.Entities;

namespace Yella.Framework.Identity.Entities;

public class Permission<TUser, TRole> : Entity<short>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>

{
    protected Permission(string name, string code, string description, string tag, ICollection<PermissionRole<TUser, TRole>> permissionRoles)
    {
        Name = name;
        Code = code;
        Description = description;
        Tag = tag;
        PermissionRoles = permissionRoles;
    }

    protected Permission(short id, string name, string code, string description, string tag, ICollection<PermissionRole<TUser, TRole>> permissionRoles) : base(id)
    {
        Name = name;
        Code = code;
        Description = description;
        Tag = tag;
        PermissionRoles = permissionRoles;
    }

    public Permission()
    {
        
    }
    [Required, MaxLength(150)] public string Name { get; set; }
    [Required, MaxLength(150)] public string Code { get; set; }
    [Required, MaxLength(150)] public string Description { get; set; }
    [Required, MaxLength(150)] public string Tag { get; set; }

    public virtual ICollection<PermissionRole<TUser, TRole>> PermissionRoles { get; set; }
}