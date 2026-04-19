namespace Mediaine.Application.Common.Models;

public class PaginationResponse<T>
{
    public IReadOnlyList<T> Items { get; set; } = [];
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalData { get; set; }
    public int TotalPages { get; set; }
    public string? Search { get; set; }
}