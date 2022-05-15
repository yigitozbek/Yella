﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Archseptia.Core.Domain.Entities;
using Archseptia.Core.Domain.Results;

namespace Archseptia.Core.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity>
    where TEntity : Entity
    {
        Task<IDataResult<TEntity>> AddAsync(TEntity entity);
        Task<IResult> AddRangeAsync(IEnumerable<TEntity> entities);
        Task<IResult> UpdateAsync(TEntity entity);
        Task<IResult> DeleteAsync(TEntity entity);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetListAsync() { return GetListAsync(null); }
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? expression);
    }
}