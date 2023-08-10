namespace Shared;

public class PaginatedResult<TListMinifiedDto>
{
    public bool IsSuccess { get; set; }
    public long CountAllDocuments { get; set; }
    public int ActualPage { get; set; }
    public int PageCount { get; set; }
    public List<TListMinifiedDto> Documents { get; set; }
    public List<string> Errors { get; set; }
    public List<string> Messages { get; set; }

    public static PaginatedResult<TListMinifiedDto> Success(List<TListMinifiedDto> data = default,
        List<string> errors = default,
        List<string> messages = default, long countDocs = default, int actualPage = default, int pageSize = default)
    {
        return new PaginatedResult<TListMinifiedDto>
        {
            CountAllDocuments = countDocs,
            Errors = errors,
            Messages = messages,
            Documents = data,
            IsSuccess = true,
            ActualPage = actualPage,
            PageCount = (int)Math.Ceiling(countDocs / (double)pageSize)
        };
    }

    public static PaginatedResult<TListMinifiedDto> Fail(List<string> errors = default, List<string> messages = default)
    {
        return new PaginatedResult<TListMinifiedDto>
        {
            CountAllDocuments = 0,
            Errors = errors,
            Messages = messages,
            Documents = default,
            IsSuccess = false
        };
    }
}