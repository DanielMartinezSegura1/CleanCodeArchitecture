namespace Shared;

public class FiltersBase
{
    public string Search { get; set; }

    public int DataPageNumber => PageNumber - 1;

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public DateTime? DateSearch { get; set; }
    public KeyValuePair<DateTime?, DateTime?> DateRangeSearch { get; set; }
}