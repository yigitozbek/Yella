#nullable enable
using System.Linq.Expressions;
using Yella.Core.Domain.Entities;
using Yella.Core.Helper.Results;

namespace Yella.Core.EntityFrameworkCore;

public interface IRepositoryBase<TEntity>
    where TEntity : Entity
{
    Task<IDataResult<TEntity>> AddAsync(TEntity entity);
    Task<IResult> AddRangeAsync(IEnumerable<TEntity> entities);
    Task<IResult> DeleteAsync(TEntity entity);
    Task<IDataResult<TEntity>> UpdateAsync(TEntity entity);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> expression );
    Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? expression = null);
    Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? expression = null, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);

}