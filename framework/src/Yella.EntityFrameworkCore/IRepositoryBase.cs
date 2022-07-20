using System.Linq.Expressions;
using Yella.Domain.Entities;
using Yella.Utilities.Results;

namespace Yella.EntityFrameworkCore;

public interface IRepositoryBase<TEntity>
    where TEntity : Entity
{
    /// <summary>
    /// This method is used to insert the data.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<IDataResult<TEntity>> AddAsync(TEntity entity);

    /// <summary>
    /// This method is used to add multiple data.
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<IResult> AddRangeAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// This method is used for delete entity 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<IResult> DeleteAsync(TEntity entity);

    /// <summary>
    /// This method is used for delete entity by query
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    Task<IResult> DeleteAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// This method is used for delete entity 
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<IResult> DeleteAsync(List<TEntity> entities);

    /// <summary>
    /// This method is used for updating entity.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<IDataResult<TEntity>> UpdateAsync(TEntity entity);

    /// <summary>
    /// This method is used for updating entities.
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<IResult> UpdateRangeAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// This method is used for getting entity. Returns a single data as a return value.
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// This method is used for getting entity. Returns a single data as a return value.
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);

    /// <summary>
    /// This method returns how many records are in the query
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// This method returns the data you are querying
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? expression = null);

    /// <summary>
    /// This method returns the data you are querying
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? expression = null, params Expression<Func<TEntity, object>>[] includes);

    /// <summary>
    /// This method is used for the absence of data. Returns a single data as a return value.
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// This method is used for the absence of data. Returns a single data as a return value.
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);

    /// <summary>
    /// This method fetches the data to witch Entities are related
    /// </summary>
    /// <param name="includes"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> WithIncludeAsync(params Expression<Func<TEntity, object>>[] includes);
}