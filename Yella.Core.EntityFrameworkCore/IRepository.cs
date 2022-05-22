using System.Linq.Expressions;
using Yella.Core.Domain.Entities;
using Yella.Core.Helper.Results;

namespace Yella.Core.EntityFrameworkCore;

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
    Task<IResult> DeleteAsync(TEntity entity);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
    Task<IEnumerable<TEntity>> GetListAsync() { return GetListAsync(null); }
    Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? expression);
}