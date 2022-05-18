namespace Yella.Core.Domain.Dto;

public class PagedResultDto<TEntity> 
{
    public PagedResultDto(long totalCount, IReadOnlyList<TEntity> _items)
    {
        Items = _items;
        TotalCount = totalCount;
    }

    public long TotalCount { get; set; }

    public readonly IReadOnlyList<TEntity> Items;

}