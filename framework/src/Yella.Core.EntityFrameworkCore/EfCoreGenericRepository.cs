using Microsoft.EntityFrameworkCore;
using Yella.Framework.Domain.Entities;

namespace Yella.Framework.EntityFrameworkCore;

public class EfCoreGenericRepository<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity>
    where TEntity : Entity
{
    public DbContext Context { get; }
    public EfCoreGenericRepository(DbContext context) : base(context) => Context = context;
}