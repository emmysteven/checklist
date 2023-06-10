namespace Checklist.Application.Common.Wrappers;

public class PaginatedResponse<T>
{
    public T Data { get; }
    public int PageNumber { get; }
    public int TotalPages { get; }

    public PaginatedResponse(T data, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling((double)pageSize);
        Data = data;
    }

    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}