using Archseptia.Core.Context;
using Archseptia.Core.Identity.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Yella.Core.Identity.Domain.Context
{
    public class IdentityDbContext : CoreDbContext<IdentityDbContext>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options, httpContextAccessor) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
            GetEntityWithoutDeleted(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }



    }


}
