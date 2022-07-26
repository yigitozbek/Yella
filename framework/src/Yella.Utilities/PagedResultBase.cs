namespace Yella.EntityFrameworkCore.Models;

public abstract class PagedResultBase
{
    public int Current { get; set; }
    public int PageCount { get; set; }
    public int Size { get; set; }
    public int RowCount { get; set; }

    public int FirstRowOnPage => (Current - 1) * Size + 1;
    public int LastRowOnPage => Math.Min(Current * Size, RowCount);
}

public class PagedResult<T> : PagedResultBase
    where T : class
{
    public IList<T> Results { get; set; }

    public PagedResult()
    {
        Results = new List<T>();
    }
}


public class PaginationFilter
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public PaginationFilter()
    {
        this.PageNumber = 1;
        this.PageSize = 10;
    }
    public PaginationFilter(int pageNumber, int pageSize)
    {
        this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
        this.PageSize = pageSize > 10 ? 10 : pageSize;
    }
}
