using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Yella.Core.Domain.Entities;
using Yella.Core.EntityFrameworkCore.Constants;
using Yella.Core.Helper.Results;

namespace Yella.Core.EntityFrameworkCore;

public class EfCoreGenericRepository<TEntity, TKey> : RepositoryBase<TEntity>, IRepository<TEntity, TKey>
    where TEntity : Entity<TKey>
    where TKey : struct
{
    private readonly DbContext _context;

    public EfCoreGenericRepository(DbContext context) : base(context) => _context = context;

    public async Task<IResult> DeleteAsync(TKey id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        _context.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
        return new SuccessResult(Messages.Removed);
    }

    public async Task<TEntity> GetAsync(TKey id)
        => await Queryable(x => x.Id.Equals(id)).FirstAsync();

    public async Task<TEntity> GetAsync(TKey id, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = Queryable(x => x.Id.Equals(id));
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.FirstAsync();
    }

    public async Task<TEntity?> FirstOrDefaultAsync(TKey id)
    {
        var query = Queryable(x => x.Id.Equals(id));
        return await query.FirstOrDefaultAsync();
    }

    public async Task<TEntity?> FirstOrDefaultAsync(TKey id, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = Queryable(x => x.Id.Equals(id));
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TEntity>> WithDetailsAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        var query = Queryable();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.ToListAsync();
    }

}