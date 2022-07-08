﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Yella.Framework.Context;
using Yella.Framework.Domain.Entities;
using Yella.Framework.EntityFrameworkCore.Constants;
using Yella.Framework.Utilities.Results;

namespace Yella.Framework.EntityFrameworkCore;

public class EfCoreGenericRepository<TEntity, TKey> : RepositoryBase<TEntity>, IRepository<TEntity, TKey>
    where TEntity : Entity<TKey>
    where TKey : struct
{
    private readonly DbContext _context;
    private readonly IApplicationDbContext _applicationDbContext;
    public EfCoreGenericRepository(DbContext context, IApplicationDbContext applicationDbContext) : base(applicationDbContext, context)
    {
        _context = context;
        _applicationDbContext = applicationDbContext;
    }


    /// <summary>
    /// This method is used for delete entity 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResult> DeleteAsync(TKey id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
#pragma warning disable CS8634
        _context.Entry(entity).State = EntityState.Deleted;
#pragma warning restore CS8634
        await _context.SaveChangesAsync();
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
    public async Task<TEntity> GetAsync(TKey id, params Expression<Func<TEntity, object>>[] includes)
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