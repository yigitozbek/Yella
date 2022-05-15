using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Archseptia.Core.Context.Extensions;
using Archseptia.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Archseptia.Core.Context
{
    public class CoreDbContext<TContext> : DbContext, IApplicationDbContext
        where TContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CoreDbContext(DbContextOptions<TContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Deleted: DeleteEntity(entry); break;
                    case EntityState.Modified: ModifiedEntity(entry); break;
                    case EntityState.Added: CreatedEntity(entry); break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected void GetEntityWithoutDeleted(ModelBuilder modelBuilder)
        {
            foreach (var type in modelBuilder.Model.GetEntityTypes())
                if (typeof(IFullAuditedEntity).IsAssignableFrom(type.ClrType)) modelBuilder.SetSoftDeleteFilter(type.ClrType);
        }

        protected void CreatedEntity(EntityEntry entry)
        {
            if (entry.Entity is not IFullAuditedEntity && entry.Entity is not ICreationAuditedEntity) return;

            entry.CurrentValues["CreationTime"] = DateTime.Now;
            if (_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier) != null)
                entry.CurrentValues["CreatorId"] = new Guid(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException());
        }

        protected void ModifiedEntity(EntityEntry entry)
        {
            if (entry.Entity is not IFullAuditedEntity && entry.Entity is not IAuditedEntity) return;

            entry.CurrentValues["LastModificationTime"] = DateTime.Now;
            if (_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier) != null)
                entry.CurrentValues["LastModifierId"] = new Guid(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException());
        }

        protected void DeleteEntity(EntityEntry entry)
        {
            if (entry.Entity is not IFullAuditedEntity) return;

            entry.State = EntityState.Modified;
            entry.CurrentValues["IsDeleted"] = true;
            entry.CurrentValues["DeletionTime"] = DateTime.Now;
            if (_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier) != null)
                entry.CurrentValues["DeleterId"] = new Guid(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException());
        }

    }
}
