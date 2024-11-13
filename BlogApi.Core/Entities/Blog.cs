
using BlogApi.Core.Enums;

namespace BlogApi.Core.Entities;

public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string slug { get; set; }
    public string Content { get; set; }
    public BlogStatusEnum BlogStatusEnum { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public User User { get; set; }
    public List<Category> Categories { get; set; }
    public List<Comment> Comments { get; set; }
}
