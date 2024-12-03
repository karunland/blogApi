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
                new() { Name = "Technology", Slug = "technology", CreatedAt = DateTime.UtcNow },
                new() { Name = "Health", Slug = "health", CreatedAt = DateTime.UtcNow },
                new() { Name = "Travel", Slug = "travel", CreatedAt = DateTime.UtcNow },
                new() { Name = "Education", Slug = "education", CreatedAt = DateTime.UtcNow },
                new() { Name = "Business", Slug = "business", CreatedAt = DateTime.UtcNow },
                new() { Name = "Science", Slug = "science", CreatedAt = DateTime.UtcNow },
                new() { Name = "Lifestyle", Slug = "lifestyle", CreatedAt = DateTime.UtcNow },
                new() { Name = "Fashion", Slug = "fashion", CreatedAt = DateTime.UtcNow },
                new() { Name = "Food", Slug = "food", CreatedAt = DateTime.UtcNow },
                new() { Name = "Sports", Slug = "sports", CreatedAt = DateTime.UtcNow },
                new() { Name = "Gaming", Slug = "gaming", CreatedAt = DateTime.UtcNow },
                new() { Name = "Movies", Slug = "movies", CreatedAt = DateTime.UtcNow },
                new() { Name = "Finance", Slug = "finance", CreatedAt = DateTime.UtcNow },
                new() { Name = "Politics", Slug = "politics", CreatedAt = DateTime.UtcNow }
            };
            context.Categories.AddRange(categories);
        }
        
        if (!await context.Blogs.AnyAsync())
        {
            var blogs = new List<Blog>
            {
                new() { Title = "The Future of AI", Content = "AI is revolutionizing the world...", Slug = "future-of-ai", Categories = new List<Category> { new() { Name = "Technology", Slug = "technology" } }, UserId = 1 },
                new() { Title = "Top 10 Healthy Foods", Content = "Eating healthy is essential for a long life...", Slug = "top-10-healthy-foods", Categories = new List<Category> { new() { Name = "Health", Slug = "health" } }, UserId = 2 },
                new() { Title = "Traveling to Japan", Content = "Explore the beauty of Japan in 2024...", Slug = "traveling-to-japan", Categories = new List<Category> { new() { Name = "Travel", Slug = "travel" } }, UserId = 3 },
                new() { Title = "The Rise of Online Education", Content = "E-learning is the new norm...", Slug = "rise-of-online-education", Categories = new List<Category> { new() { Name = "Education", Slug = "education" } }, UserId = 4 },
                new() { Title = "How to Start a Business", Content = "Tips for entrepreneurs...", Slug = "how-to-start-a-business", Categories = new List<Category> { new() { Name = "Business", Slug = "business" } }, UserId = 1 },
                new() { Title = "Space Exploration Updates", Content = "NASA announces new missions...", Slug = "space-exploration-updates", Categories = new List<Category> { new() { Name = "Science", Slug = "science" } }, UserId = 2 },
                new() { Title = "Minimalist Lifestyle", Content = "Declutter your life with minimalism...", Slug = "minimalist-lifestyle", Categories = new List<Category> { new() { Name = "Lifestyle", Slug = "lifestyle" } }, UserId = 3 },
                new() { Title = "Summer Fashion Trends", Content = "Stay stylish with these trends...", Slug = "summer-fashion-trends", Categories = new List<Category> { new() { Name = "Fashion", Slug = "fashion" } }, UserId = 4 },
                new() { Title = "Top Recipes for Foodies", Content = "Delicious recipes to try at home...", Slug = "top-recipes-for-foodies", Categories = new List<Category> { new() { Name = "Food", Slug = "food" } }, UserId = 1 },
                new() { Title = "The History of Soccer", Content = "From its origins to modern times...", Slug = "history-of-soccer", Categories = new List<Category> { new() { Name = "Sports", Slug = "sports" } }, UserId = 2 },
                new() { Title = "Gaming in 2024", Content = "Upcoming games and consoles...", Slug = "gaming-in-2024", Categories = new List<Category> { new() { Name = "Gaming", Slug = "gaming" } }, UserId = 3 },
                new() { Title = "Top Movies to Watch", Content = "Don't miss these blockbusters...", Slug = "top-movies-to-watch", Categories = new List<Category> { new() { Name = "Movies", Slug = "movies" } }, UserId = 4 },
                new() { Title = "Investing Basics", Content = "Learn how to grow your wealth...", Slug = "investing-basics", Categories = new List<Category> { new() { Name = "Finance", Slug = "finance" } }, UserId = 1 },
                new() { Title = "Politics in 2024", Content = "Key political events and elections...", Slug = "politics-in-2024", Categories = new List<Category> { new() { Name = "Politics", Slug = "politics" } }, UserId = 2 }
            };

            context.Blogs.AddRange(blogs);
        }
        await context.SaveChangesAsync();
    }
}