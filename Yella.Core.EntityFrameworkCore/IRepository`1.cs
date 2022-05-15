#nullable enable
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Archseptia.Core.Domain.Entities;
using Archseptia.Core.Domain.Results;

namespace Archseptia.Core.EntityFrameworkCore
{
    public interface IRepository<TEntity, in TKey> : IRepositoryBase<TEntity>
        where TEntity : Entity<TKey>
        where TKey : struct
    {
        Task<IResult> DeleteAsync(TKey id);
        
        Task<TEntity> GetAsync(TKey id);
        Task<TEntity> GetAsync(TKey id, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity?> FirstOrDefaultAsync(TKey id);
        Task<TEntity?> FirstOrDefaultAsync(TKey id, params Expression<Func<TEntity, object>>[] includes);
        
        Task<IEnumerable<TEntity>> GetListAsync() { return GetListAsync(null); }
        Task<IEnumerable<TEntity>> WithDetailsAsync(params Expression<Func<TEntity, object>>[] includes);
    
    }
}
