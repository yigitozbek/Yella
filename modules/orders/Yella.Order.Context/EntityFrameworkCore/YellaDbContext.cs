using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yella.Framework.Context;
using Yella.Order.Domain.Orders;

namespace Yella.Order.Context.EntityFrameworkCore
{
    public class YellaDbContext : CoreDbContext<YellaDbContext>
    {
        public YellaDbContext(DbContextOptions<YellaDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options, httpContextAccessor)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(YellaDbContext).Assembly);

            GetEntityWithoutDeleted(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored));

        public DbSet<Domain.Orders.OrderItem> Orders { get; set; }


    }
}
