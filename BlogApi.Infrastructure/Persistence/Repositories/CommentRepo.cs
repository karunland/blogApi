using BlogApi.Application.Common.Messages;
using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Comment;
using BlogApi.Application.Interfaces;
using BlogApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.Persistence.Repositories;

public class CommentRepo(BlogContext context, ICurrentUserService currentUserService)
{
    public async Task<ApiResult> Create(CommentAddDto comment)
    {
        var newComment = new Comment
        {
            BlogId = comment.BlogId,
            Content = comment.Content,
            UserId = currentUserService.Id,
            CreatedAt = DateTime.Now,
        };

        await context.Comments.AddAsync(newComment);
        await context.SaveChangesAsync();

        return ApiResult.Success();
    }
    
    public async Task<ApiResultPagination<CommentsDto>> GetByBlogId(string slug, FilterModel filter)
    {
        var comments = context.Comments
            .Where(x => x.Blog.slug == slug)
            .Select(x => new CommentsDto
            {
                Id = x.Id,
                Content = x.Content,
                CreatedAt = x.CreatedAt,
                AuthorName = x.User.FullName
            });

        return await comments.PaginatedListAsync(filter.PageNumber, filter.PageSize);
    }
    
    public async Task<ApiResult> Update(int id, CommentAddDto comment)
    {
        var commentToUpdate = await context.Comments.SingleOrDefaultAsync(x => x.Id == id && x.UserId == currentUserService.Id);
        if (commentToUpdate == null)
        {
            return ApiError.Failure(Messages.NotFound);
        }

        commentToUpdate.Content = comment.Content;
        await context.SaveChangesAsync();

        return ApiResult.Success();
    }
    
    public async Task<ApiResult> Delete(int id)
    {
        var comment = await context.Comments.SingleOrDefaultAsync(x => x.UserId == currentUserService.Id && x.Id == id);
        if (comment == null)
        {
            return ApiError.Failure(Messages.NotFound);
        }

        context.Comments.Remove(comment);
        await context.SaveChangesAsync();

        return ApiResult.Success();
    }
}