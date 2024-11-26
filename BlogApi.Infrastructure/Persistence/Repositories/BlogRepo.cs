using BlogApi.Application.Common.Messages;
using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Blog;
using BlogApi.Application.DTOs.Category;
using BlogApi.Application.Interfaces;
using BlogApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.Persistence.Repositories;

public class BlogRepo(BlogContext context, ICurrentUserService currentUserService)
{
    public async Task<ApiResultPagination<BlogsDto>> GetAll(BlogFilterModel filter)
    {
        var blogs = context.Blogs
            .OrderByDescending(x => x.UpdatedAt)
            .ThenByDescending(x => x.CreatedAt)
            .Select(x => new BlogsDto
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                CreatedAt = x.CreatedAt,
                AuthorName = x.User.FullName,
                slug = x.slug,
                UpdatedAt = x.UpdatedAt ?? x.CreatedAt
            });

        if (!string.IsNullOrEmpty(filter.Search))
        {
            blogs = blogs.Where(x => x.Title.Contains(filter.Search));
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
            BlogStatusEnum = blog.Status,
            slug = blog.Title,
            CreatedAt = DateTime.UtcNow,
        };

        await context.Blogs.AddAsync(newBlog);
        await context.SaveChangesAsync();

        return ApiResult.Success();
    }

    public async Task<ApiResultPagination<BlogsDto>> MyBlogs(FilterModel filter)
    {
        var blogs = context.Blogs
            .Where(x => x.UserId == currentUserService.Id)
            .OrderByDescending(x => x.UpdatedAt)
            .ThenByDescending(x => x.CreatedAt)
            .Select(x => new BlogsDto
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                CreatedAt = x.CreatedAt,
                AuthorName = x.User.FullName,
                slug = x.slug
            });

        return await blogs.PaginatedListAsync(filter.PageNumber, filter.PageSize);
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
        blogToUpdate.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();

        return ApiResult.Success();
    }

    public async Task<ApiResult> Delete(string slug)
    {
        var blogToDelete = await context.Blogs.FindAsync(slug);

        if (blogToDelete == null)
        {
            return ApiError.Failure(Messages.NotFound);
        }

        blogToDelete.IsDeleted = true;
        blogToDelete.DeletedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();

        return ApiResult.Success();
    }
    
    public async Task<ApiResult<BlogsDto>> Detail(string slug)
    {
        var blog = await context.Blogs
            .Where(x => x.slug == slug)
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
            return ApiError.Failure(Messages.NotFound);

        return blog;
    }
}