using BlogApi.Application.Common.Messages;
using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Blog;
using BlogApi.Application.Interfaces;
using BlogApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.Persistence.Repositories;

public class BlogRepo(BlogContext context, ICurrentUserService currentUserService)
{
    public async Task<ApiResultPagination<BlogsDto>> GetAll(BlogFilterModel filter)
    {
        var blogs = context.Categories.Where(x => filter.CategoryIds.Contains(x.Id))
            .Select(x => new BlogsDto
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                CreatedAt = x.CreatedAt,
                AuthorName = x.User.FullName
            });
            

        if (!string.IsNullOrEmpty(filter.Search))
        {
            blogs = blogs.Where(x => x.Title.Contains(filter.Search));
        }
        
        // category filter
        if (filter.CategoryIds.Count > 0)
        {
            blogs = blogs.Where(a => a.AuthorName));
        }

        return await blogs.PaginatedListAsync(filter.PageNumber, filter.PageSize);
    }

    public async Task<ApiResult> Create(BlogAddDto blog)
    {
        var newBlog = new Blog
        {
            Title = blog.Title,
            Content = blog.Content,
            UserId = currentUserService.Id,
            slug = blog.Title,
            CreatedAt = DateTime.Now,
        };

        await context.Blogs.AddAsync(newBlog);
        await context.SaveChangesAsync();

        return ApiResult.Success();
    }

    public async Task<ApiResultPagination<BlogsDto>> GetBySlug(string slug, int page, int pageSize)
    {
        var blogs = context.Blogs
            .Where(x => x.slug == slug)
            .Select(x => new BlogsDto
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                CreatedAt = x.CreatedAt,
                AuthorName = x.User.FullName
            });

        return await blogs.PaginatedListAsync(page, pageSize);
    }

    public async Task<ApiResultPagination<BlogsDto>> MyBlogs(int page, int pageSize)
    {
        var blogs = context.Blogs
            .Where(x => x.UserId == currentUserService.Id)
            .Select(x => new BlogsDto
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                CreatedAt = x.CreatedAt,
                AuthorName = x.User.FullName
            });

        return await blogs.PaginatedListAsync(page, pageSize);
    }

    public async Task<ApiResult> Update(BlogUpdateDto blog)
    {
        var blogToUpdate = await context.Blogs.FindAsync(blog.Id);

        if (blogToUpdate == null)
        {
            return ApiError.Failure(Messages.NotFound);
        }

        blogToUpdate.Title = blog.Title;
        blogToUpdate.Content = blog.Content;
        blogToUpdate.UpdatedAt = DateTime.Now;

        await context.SaveChangesAsync();

        return ApiResult.Success();
    }

    public async Task<ApiResult> Delete(int id)
    {
        var blogToDelete = await context.Blogs.FindAsync(id);

        if (blogToDelete == null)
        {
            return ApiError.Failure(Messages.NotFound);
        }

        // TODO: Context interceptor for soft delete
        blogToDelete.IsDeleted = true;
        blogToDelete.DeletedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();

        return ApiResult.Success();
    }
    
    public async Task<ApiResult<BlogsDto>> Detail(int id)
    {
        var blog = await context.Blogs
            .Where(x => x.Id == id)
            .Select(x => new BlogsDto
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                CreatedAt = x.CreatedAt,
                AuthorName = x.User.FullName
            })
            .FirstOrDefaultAsync();

        if (blog == null)
        {
            return ApiError.Failure(Messages.NotFound);
        }

        return blog;
    }
}