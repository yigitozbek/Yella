using Microsoft.EntityFrameworkCore;
using Yella.Context;
using Yella.Domain.Entities;

namespace Yella.EntityFrameworkCore;

public class EfCoreGenericRepository<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity>
    where TEntity : Entity
{
    public EfCoreGenericRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext) { }
}