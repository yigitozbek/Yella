using Microsoft.EntityFrameworkCore;

namespace Yella.Framework.Context;

public interface IApplicationDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}