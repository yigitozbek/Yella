using Microsoft.EntityFrameworkCore;
using Yella.Context;
using Yella.Domain.Entities;

namespace Yella.EntityFrameworkCore;

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