using Archseptia.Core.Context;
using Archseptia.Core.Identity.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Yella.Core.Identity.Domain.Entities;

namespace Yella.Core.Identity.Domain.Context
{
    public class IdentityDbContext<TUser, TRole> : CoreDbContext<IdentityDbContext<TUser, TRole>>
        where TUser : IdentityUser<TUser, TRole>, new()
        where TRole : IdentityRole<TUser, TRole>, new()

    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext<TUser, TRole>> options, IHttpContextAccessor httpContextAccessor) : base(options, httpContextAccessor) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext<TUser, TRole>).Assembly);
            GetEntityWithoutDeleted(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TUser> Users { get; set; }
        public DbSet<TRole> Roles { get; set; }
        public DbSet<UserRole<TUser, TRole>> UserRoles { get; set; }
        public DbSet<Permission<TUser, TRole>> Permissions { get; set; }
        public DbSet<PermissionRole<TUser, TRole>> PermissionRoles { get; set; }
        public DbSet<UserLoginLog<TUser, TRole>> UserLoginLogs { get; set; }
    }
}
