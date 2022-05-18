using System.ComponentModel.DataAnnotations.Schema;
using Yella.Core.Domain.Entities;

namespace Yella.Core.Identity.Domain.Entities;

public class UserRole<TUser, TRole> : FullAuditedEntity<Guid>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{
    [ForeignKey(nameof(UserId))]
    public virtual TUser User { get; set; }
    public Guid UserId { get; set; }


    [ForeignKey(nameof(RoleId))]
    public virtual TRole Role { get; set; }
    public Guid RoleId { get; set; }
}