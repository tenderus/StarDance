namespace StarDance.Common.Helpers;

public class PagedRequest
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public string ColumnNameForSorting { get; set; }

    public string SortDirection { get; set; }

    public RequestFilters RequestFilters { get; set;}

    public PagedRequest()
    {
        RequestFilters = new RequestFilters();
    }
}