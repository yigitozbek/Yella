using System.Collections.Generic;

namespace Archseptia.Core.Domain.Entities
{
    public class PagedResult<TEntity> 
    {
        public PagedResult(long totalCount, IReadOnlyList<TEntity> items)
        {
            _items = items;
            TotalCount = totalCount;
        }

        public long TotalCount { get; set; }

        private readonly IReadOnlyList<TEntity> _items;

    }
}
