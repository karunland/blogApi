namespace BlogApi.Application.DTOs.Comment;

public record CommentAddDto
{
    public int BlogId { get; set; }
    public string Content { get; set; }
}