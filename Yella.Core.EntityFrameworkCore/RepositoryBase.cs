using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Yella.Core.Domain.Entities;
using Yella.Core.Domain.Results;
using Yella.Core.EntityFrameworkCore.Constants;

namespace Yella.Core.EntityFrameworkCore
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : Entity
    {
        private readonly DbContext _context;
        public RepositoryBase(DbContext context) => _context = context;

        public async Task<IDataResult<TEntity>> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return new DataResult<TEntity>(entity, true, Messages.Added);
        }

        public async Task<IResult> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return new SuccessResult();
        }

        public async Task<IResult> DeleteAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return new SuccessResult(Messages.Removed);
        }

        public async Task<IResult> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new SuccessResult(Messages.Updated);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
            => await Queryable(expression).FirstAsync();

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression ) 
            => await Queryable(expression).CountAsync();

        protected IQueryable<TEntity> Queryable() =>
            _context.Set<TEntity>() as IQueryable<TEntity>;
        protected IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> expression) =>
            _context.Set<TEntity>().Where(expression) as IQueryable<TEntity>;

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? expression = null) =>
            expression != null
                ? await Queryable(expression).ToListAsync()
                : await Queryable().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? expression = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = expression != null
                ? Queryable(expression)
                : Queryable();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.ToListAsync();
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
            => await Queryable(expression).FirstOrDefaultAsync();
    }

}
