#nullable enable
using System.Linq.Expressions;
using Yella.Framework.Domain.Entities;
using Yella.Framework.Helper.Results;

namespace Yella.Framework.EntityFrameworkCore;

public interface IRepository<TEntity, in TKey> : IRepositoryBase<TEntity>
    where TEntity : Entity<TKey>
    where TKey : struct
{
    /// <summary>
    /// This method is used for delete entity 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResult> DeleteAsync(TKey id);

    /// <summary>
    /// This method is used for getting entity. Returns a single data as a return value.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity> GetAsync(TKey id);

    /// <summary>
    /// This method is used for getting entity. Returns a single data as a return value.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    Task<TEntity> GetAsync(TKey id, params Expression<Func<TEntity, object>>[] includes);

    /// <summary>
    /// This method is used for the absence of data. Returns a single data as a return value.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity?> FirstOrDefaultAsync(TKey id);

    /// <summary>
    /// This method is used for the absence of data. Returns a single data as a return value.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    Task<TEntity?> FirstOrDefaultAsync(TKey id, params Expression<Func<TEntity, object>>[] includes);
 
    
}