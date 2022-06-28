using System.Linq.Expressions;
using Yella.Framework.Domain.Entities;
using Yella.Framework.Helper.Results;

namespace Yella.Framework.EntityFrameworkCore;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepository<TEntity>
    where TEntity : Entity
{
    Task<IDataResult<TEntity>> AddAsync(TEntity entity);
    Task<IResult> AddRangeAsync(IEnumerable<TEntity> entities);
    Task<IDataResult<TEntity>> UpdateAsync(TEntity entity);

    /// <summary>
    /// This method is used for delete entity 
    /// </summary>
    /// <returns></returns>
    Task<IResult> DeleteAsync(TEntity entity);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
    Task<IEnumerable<TEntity>> GetListAsync() { return GetListAsync(null); }
    Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? expression);
}