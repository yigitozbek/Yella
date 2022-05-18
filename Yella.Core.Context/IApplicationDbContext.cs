using Microsoft.EntityFrameworkCore;

namespace Yella.Core.Context;

public interface IApplicationDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}