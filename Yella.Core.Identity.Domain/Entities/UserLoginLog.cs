using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using Archseptia.Core.Domain.Entities;
using Yella.Core.Identity.Domain.Entities;

namespace Archseptia.Core.Identity.Domain.Entities
{
    public class UserLoginLog<TUser, TRole> : Entity<long>
        where TUser : IdentityUser<TUser, TRole>
        where TRole : IdentityRole<TUser, TRole>
    {
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual TUser User { get; set; }
         
        public string UserAgent { get; set; }

        public DateTime LoginDate { get; set; }

        public IPAddress IpAddress { get; set; }

        public bool IsSuccessful { get; set; }
    }
}