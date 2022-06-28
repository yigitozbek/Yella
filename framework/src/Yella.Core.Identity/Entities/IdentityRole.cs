using System.ComponentModel.DataAnnotations;
using Yella.Framework.Domain.Entities;

namespace Yella.Framework.Identity.Entities;

public class IdentityRole<TUser, TRole> : FullAuditedEntity<Guid>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{

    public string Name { get; set; }

    public string Description { get; set; }

    public string Code { get; set; }

    public virtual ICollection<UserRole<TUser, TRole>> UserRoles { get; set; }
}