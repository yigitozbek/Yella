using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Yella.Context;
using Yella.Domain.Entities;
using Yella.EntityFrameworkCore.Constants;
using Yella.Utilities.Results;

namespace Yella.EntityFrameworkCore;

public class EfCoreGenericRepository<TEntity, TKey> : RepositoryBase<TEntity>, IRepository<TEntity, TKey>
    where TEntity : Entity<TKey>
    where TKey : struct
{
    private readonly IApplicationDbContext _applicationDbContext;
    public EfCoreGenericRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    /// <summary>
    /// This method is used for delete entity 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResult> DeleteAsync(TKey id)
    {
        var entity = await _applicationDbContext.Set<TEntity>().FindAsync(id);
        _applicationDbContext.Entry(entity).State = EntityState.Deleted;
        await _applicationDbContext.SaveChangesAsync();
        return new SuccessResult(CrudMessage.Removed);
    }

    /// <summary>
    /// This method is used for getting entity. Returns a single data as a return value.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<TEntity> GetAsync(TKey id)
    {
        var query = await _applicationDbContext.Queryable<TEntity>(x => x.Id.Equals(id)).FirstAsync();
        return query;
    }

    /// <summary>
    /// This method is used for getting entity. Returns a single data as a return value.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    public async Task<TEntity> GetAsync(TKey id, params Expression<Func<TEntity, object>>[]? includes)
    {
        var query = _applicationDbContext.Queryable<TEntity>(x => x.Id.Equals(id));
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.FirstAsync();
    }

    /// <summary>
    /// This method is used for the absence of data. Returns a single data as a return value.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<TEntity?> FirstOrDefaultAsync(TKey id)
    {
        var query = _applicationDbContext.Queryable<TEntity>(x => x.Id.Equals(id));
        return await query.FirstOrDefaultAsync();
    }

    /// <summary>
    /// This method is used for the absence of data. Returns a single data as a return value.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    public async Task<TEntity?> FirstOrDefaultAsync(TKey id, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _applicationDbContext.Queryable<TEntity>(x => x.Id.Equals(id));
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.FirstOrDefaultAsync();
    }


}