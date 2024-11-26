namespace BlogApi.Application.DTOs.Blog;

public record BlogFilterModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 9;
    public string? Search { get; set; }
    public List<int>? CategoryIds { get; set; }
}
