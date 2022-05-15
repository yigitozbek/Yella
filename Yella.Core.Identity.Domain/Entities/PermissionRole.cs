using System;
using System.ComponentModel.DataAnnotations.Schema;
using Archseptia.Core.Domain.Entities;
using Yella.Core.Identity.Domain.Entities;

namespace Archseptia.Core.Identity.Domain.Entities
{
    public class PermissionRole<TUser, TRole> : FullAuditedEntity<Guid>
        where TUser : IdentityUser<TUser, TRole>
        where TRole : IdentityRole<TUser, TRole>
    {
        [ForeignKey(nameof(PermissionId))]
        public virtual Permission<TUser, TRole> Permission { get; set; }
        public short PermissionId { get; set; }


        [ForeignKey(nameof(RoleId))]
        public virtual TRole Role { get; set; }
        public Guid RoleId { get; set; }
    }
}