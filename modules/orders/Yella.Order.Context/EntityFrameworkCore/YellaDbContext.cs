using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Yella.Context;
using Yella.Domain.Entities;
using Yella.Identity.Entities;
using Yella.Order.Domain;
using Yella.Order.Domain.Identities;
using Yella.Order.Domain.Orders;

namespace Yella.Order.Context.EntityFrameworkCore
{
    public class YellaDbContext : CoreDbContext<YellaDbContext>, IApplicationDbContext
    {
        public YellaDbContext(DbContextOptions<YellaDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options, httpContextAccessor)
        {

        }

        public new IQueryable<TEntity> Queryable<TEntity>(Expression<Func<TEntity, bool>>? expression = null)
            where TEntity : Entity
        {
            var query = expression != null
                ? Set<TEntity>().Where(expression)
                : Set<TEntity>();

            if (typeof(ICompanyBase).IsAssignableFrom(typeof(TEntity)))
            {
                query = query.Where(
                    x => ((ICompanyBase)x).CompanyId == new Guid("2AC9F9B6-E32A-4CBD-8C3A-7FAE94BF20C1"));
            }

            return query;
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

        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Demand> Demands { get; set; }


        #region Identities
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole<User, Role>> UserRoles { get; set; }
        public DbSet<Permission<User, Role>> Permissions { get; set; }
        public DbSet<PermissionRole<User, Role>> PermissionRoles { get; set; }
        public DbSet<UserLoginLog<User, Role>> UserLoginLogs { get; set; }
        #endregion

    }
}
