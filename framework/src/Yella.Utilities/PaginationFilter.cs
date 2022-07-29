using System.Linq.Expressions;

namespace Yella.Utilities;

public class PaginationFilter<TEntity>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public Expression<Func<TEntity, object>> OrderBy { get; set; }
    public bool IsDesc { get; set; }

    public PaginationFilter()
    {
        this.CurrentPage = 1;
        this.PageSize = 10;
    }

    public PaginationFilter(int pageNumber, int pageSize)
    {
        CurrentPage = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize;
    }
}