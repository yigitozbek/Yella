using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Yella.Domain.Entities;

namespace Yella.Context;

public interface IApplicationDbContext
{
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
    IQueryable<TEntity> Queryable<TEntity>(Expression<Func<TEntity, bool>>? expression = null) where TEntity : Entity;
}