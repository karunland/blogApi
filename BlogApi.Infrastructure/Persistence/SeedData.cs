using System.Runtime.InteropServices.JavaScript;
using BlogApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.Persistence;

public static class SeedData
{
    public static async Task SeedDatabaseAsync(this BlogContext context)
    {
        if (!await context.Users.AnyAsync())
        {
            var users = new List<User>
            {
                new()
                {
                    Id = 1,
                    FirstName = "Harun",
                    LastName = "Korkmaz",
                    Username = "harunkorkmaz",
                    Email = "1harunkorkmaz@gmail.com",
                    Password = "7C4A8D09CA3762AF61E59520943DC26494F8941B",
                },
                new()
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    Username = "johndoe",
                    Email = "johndoe@gmail.com",
                    Password = "7C4A8D09CA3762AF61E59520943DC26494F8941B",
                }
            };
            context.Users.AddRange(users);
        }
        
        if (!await context.Categories.AnyAsync())
        {
            var categories = new List<Category>
            {
                new()
                {
                    Name = "Category 1",
                    Slug = "category-1",
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Name = "Category 2",
                    Slug = "category-2",
                    CreatedAt = DateTime.UtcNow
                }
            };
            context.Categories.AddRange(categories);
        }
        
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
                    Categories = new List<Category>()
                    {
                        new()
                        {
                            Name = "Category 1",
                            Slug = "category-1",

                            CreatedAt = DateTime.UtcNow
                        },
                        new()
                        {
                            Name = "Category 2",
                            Slug = "category-2",
                            CreatedAt = DateTime.UtcNow
                        }
                    }
                    ,
                    UserId = 1,
                    Comments =
                    [
                        new Comment()
                        {
                            Content = "Comment Content",
                            UserId = 2
                        },
                        new Comment()
                        {
                            Content = "thx jhoe",
                            UserId = 1,
                        }
                    ],
                },
                new()
                {
                    Title = "Blog Title",
                    Content =
                        "Blog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog Content",
                    slug = "Blog-Title",
                    Categories = new List<Category>()
                    {
                        new()
                        {
                            Name = "Category 1",
                            Slug = "category-1",
                            CreatedAt = DateTime.UtcNow
                        },
                        new()
                        {
                            Name = "Category 2",
                            Slug = "category-2",
                            CreatedAt = DateTime.UtcNow
                        }
                    }
                    ,
                    UserId = 1,
                    Comments =
                    [
                        new Comment()
                        {
                            Content = "Comment Content",
                            UserId = 2
                        },
                        new Comment()
                        {
                            Content = "thx jhoe",
                            UserId = 1,
                        }
                    ],
                },
                new()
                {
                    Title = "Blog Title",
                    Content =
                        "Blog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog ContentBlog Content",
                    slug = "Blog-Title",
                    Categories = new List<Category>()
                    {
                        new()
                        {
                            Name = "Category 1",
                            Slug = "category-1",
                            CreatedAt = DateTime.UtcNow
                        },
                        new()
                        {
                            Name = "Category 2",
                            Slug = "category-2",
                            CreatedAt = DateTime.UtcNow
                        }
                    },
                    UserId = 1,
                    Comments =
                    [
                        new Comment()
                        {
                            Content = "Comment Content",
                            UserId = 2
                        },
                        new Comment()
                        {
                            Content = "thx jhoe",
                            UserId = 1,
                        }
                    ],
                },
                
            };

            context.Blogs.AddRange(blogs);
        }
        await context.SaveChangesAsync();
    }
}