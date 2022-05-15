using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Archseptia.Core.Domain.Entities;
using Yella.Core.Identity.Domain.Entities;

namespace Archseptia.Core.Identity.Domain.Entities
{
    public class Permission<TUser, TRole> : Entity<short>
        where TUser : IdentityUser<TUser, TRole>
        where TRole : IdentityRole<TUser, TRole>

    {
        [Required, MaxLength(150)] public string Name { get; set; }
        [Required, MaxLength(150)] public string Code { get; set; }
        [Required, MaxLength(150)] public string Description { get; set; }
        [Required, MaxLength(150)] public string Tag { get; set; }

        public virtual ICollection<PermissionRole<TUser, TRole>> PermissionRoles { get; set; }
    }
}