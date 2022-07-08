using Microsoft.EntityFrameworkCore;
using Yella.Framework.Context;
using Yella.Framework.Domain.Entities;

namespace Yella.Framework.EntityFrameworkCore;

public class EfCoreGenericRepository<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity>
    where TEntity : Entity
{
    private IApplicationDbContext _applicationDbContext;

    private DbContext _dbContext;

    public EfCoreGenericRepository(IApplicationDbContext applicationDbContext, DbContext dbContext) : base(applicationDbContext, dbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbContext = dbContext;
    }

}