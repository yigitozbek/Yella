using Microsoft.EntityFrameworkCore;
using Yella.Core.Domain.Entities;

namespace Yella.Core.EntityFrameworkCore;

public class EfCoreGenericRepository<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity>
    where TEntity : Entity
{
    private readonly DbContext _context;
    public EfCoreGenericRepository(DbContext context) : base(context) => _context = context;
}