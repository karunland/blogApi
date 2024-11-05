using System.Runtime.InteropServices.JavaScript;
using BlogApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.Persistence;

public static class SeedData
{
    public static async Task SeedDatabaseAsync(this BlogContext context)
    {
        if (!await context.Blogs.AnyAsync())
        {
            var blogs = new List<Blog>
            {
                new()
                {
                    Title = "Blog Title",
                    Content =
                        "Blog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog Content",
                    slug = "Blog-Title",
                    Categories =
                    [
                        new()
                        {
                            Name = "Technology",
                            Slug = "Technology"
                        }
                    ],
                    User =
                        new()
                        {
                            Id = 1,
                            FirstName = "Harun",
                            LastName = "Korkmaz",
                            Username = "harunkorkmaz",
                            Email = "1harunkorkmaz@gmail.com",
                            Password = "7C4A8D09CA3762AF61E59520943DC26494F8941B",
                        },
                    Comments =
                    [
                        new Comment()
                        {
                            Content = "Comment Content",
                            User = new()
                            {
                                Id = 2,
                                FirstName = "John",
                                LastName = "Doe",
                                Username = "commenter 1",
                                Email = "ss@gmail.com",
                                Password = "7C4A8D09CA3762AF61E59520943DC26494F8941B",
                            }
                        }
                    ],
                }
            };

            context.Blogs.AddRange(blogs);
        }
        await context.SaveChangesAsync();
    }
}