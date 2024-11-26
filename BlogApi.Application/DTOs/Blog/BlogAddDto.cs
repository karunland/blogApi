using BlogApi.Core.Enums;

namespace BlogApi.Application.DTOs.Blog;

public record BlogAddDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public BlogStatusEnum Status { get; set; }
}
