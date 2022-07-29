﻿namespace Yella.Utilities;

public abstract class PagedResultBase
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public int FirstRowOnPage => (CurrentPage - 1) * PageSize + 1;
    public int LastRowOnPage => Math.Min(CurrentPage * PageSize, TotalCount);
}