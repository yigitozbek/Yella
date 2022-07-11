using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Yella.Context;
using Yella.Domain.Entities;
using Yella.EntityFrameworkCore.Constants;
using Yella.Utilities.Results;

namespace Yella.EntityFrameworkCore;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    where TEntity : Entity
{
    private readonly IApplicationDbContext _applicationDbContext;
    public RepositoryBase(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    /// <summary>
    /// This method is used to insert the data.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<IDataResult<TEntity>> AddAsync(TEntity entity)
    {
        await _applicationDbContext.Set<TEntity>().AddAsync(entity);

        await _applicationDbContext.SaveChangesAsync();

        return new DataResult<TEntity>(entity, true, CrudMessage.Added);
    }

    /// <summary>
    /// This method is used to add multiple data.
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public async Task<IResult> AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _applicationDbContext.Set<TEntity>().AddRangeAsync(entities);

        await _applicationDbContext.SaveChangesAsync();

        return new SuccessResult();
    }

    /// <summary>
    /// This method is used for updating entities.
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public async Task<IResult> UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        _applicationDbContext.Set<TEntity>().UpdateRange(entities);

        await _applicationDbContext.SaveChangesAsync();

        return new SuccessResult();
    }

    /// <summary>
    /// This method is used for the absence of data. Returns a single data as a return value.
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _applicationDbContext.Queryable<TEntity>(expression);

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        return await query.FirstOrDefaultAsync();
    }

    /// <summary>
    /// This method fetches the data to witch Entities are related
    /// </summary>
    /// <param name="includes"></param>
    /// <returns></returns>
    public async Task<IEnumerable<TEntity>> WithIncludeAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _applicationDbContext.Queryable<TEntity>();

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        return await query.ToListAsync();
    }

    /// <summary>
    /// This method is used for delete entity 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<IResult> DeleteAsync(TEntity entity)
    {

        _applicationDbContext.Entry(entity).State = EntityState.Deleted;

        await _applicationDbContext.SaveChangesAsync();

        return new SuccessResult(CrudMessage.Removed);
    }

    /// <summary>
    /// This method is used for delete entities 
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public async Task<IResult> DeleteRangeAsync(List<TEntity> entities)
    {
        _applicationDbContext.Set<TEntity>().RemoveRange(entities);
        await _applicationDbContext.SaveChangesAsync();
        return new SuccessResult(CrudMessage.Successful);
    }

    /// <summary>
    /// This method is used for updating entity.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<IDataResult<TEntity>> UpdateAsync(TEntity entity)
    {
        _applicationDbContext.Entry(entity).State = EntityState.Modified;

        await _applicationDbContext.SaveChangesAsync();

        return new SuccessDataResult<TEntity>(entity, CrudMessage.Updated);
    }

    /// <summary>
    /// This method is used for getting entity. Returns a single data as a return value.
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
    {

        var query = await _applicationDbContext.Queryable(expression).FirstAsync();

        return query;
    }

    /// <summary>
    /// This method is used for getting entity. Returns a single data as a return value.
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _applicationDbContext.Queryable(expression);

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        return await query.FirstAsync();
    }

    /// <summary>
    /// This method returns how many records are in the query
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
    {
        var query = await _applicationDbContext.Queryable(expression).CountAsync();
        return query;
    }

    /// <summary>
    /// This method returns the data you are querying
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? expression = null)
    {
        var query =
         expression != null
            ? await _applicationDbContext.Queryable(expression).ToListAsync()
            : await _applicationDbContext.Queryable<TEntity>().ToListAsync();

        return query;
    }

    /// <summary>
    /// This method returns the data you are querying
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? expression = null, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = expression != null
            ? _applicationDbContext.Queryable(expression)
            : _applicationDbContext.Queryable<TEntity>();

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        return await query.ToListAsync();
    }

    /// <summary>
    /// This method is used for the absence of data. Returns a single data as a return value.
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
    {
        var query = await _applicationDbContext.Queryable(expression).FirstOrDefaultAsync();
        return query;
    }



}