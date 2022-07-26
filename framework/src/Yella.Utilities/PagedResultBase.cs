namespace Yella.Utilities;

public abstract class PagedResultBase
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public int FirstRowOnPage => (CurrentPage - 1) * PageSize + 1;
    public int LastRowOnPage => Math.Min(CurrentPage * PageSize, TotalCount);
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
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }

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
