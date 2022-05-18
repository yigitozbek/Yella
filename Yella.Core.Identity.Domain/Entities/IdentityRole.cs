using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Yella.Core.Domain.Entities;

namespace Yella.Core.Identity.Domain.Entities;

public class IdentityRole<TUser, TRole> : FullAuditedEntity<Guid>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{
    [Required, MinLength(5), MaxLength(50)]
    public string Name { get; set; }

    public string Description { get; set; }

    [MaxLength(250)]
    public string Code { get; set; }

    public virtual ICollection<UserRole<TUser, TRole>> UserRoles { get; set; }
}