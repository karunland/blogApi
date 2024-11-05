using BlogApi.Application.Common.Messages;
using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Comment;
using BlogApi.Application.Interfaces;
using BlogApi.Core.Entities;

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
    
    public async Task<ApiResultPagination<CommentsDto>> GetByBlogId(int blogId, int page, int pageSize)
    {
        var comments = context.Comments
            .Where(x => x.BlogId == blogId)
            .Select(x => new CommentsDto
            {
                Id = x.Id,
                Content = x.Content,
                CreatedAt = x.CreatedAt,
                AuthorName = x.User.FullName
            });

        return await comments.PaginatedListAsync(page, pageSize);
    }
    
    public async Task<ApiResult> Delete(int id)
    {
        var comment = await context.Comments.FindAsync(id);
        if (comment == null)
        {
            return ApiError.Failure(Messages.NotFound);
        }

        context.Comments.Remove(comment);
        await context.SaveChangesAsync();

        return ApiResult.Success();
    }
    
    public async Task<ApiResult> Update(int id, CommentAddDto comment)
    {
        var commentToUpdate = await context.Comments.FindAsync(id);
        if (commentToUpdate == null)
        {
            return ApiError.Failure(Messages.NotFound);
        }

        commentToUpdate.Content = comment.Content;
        await context.SaveChangesAsync();

        return ApiResult.Success();
    }
    
    public async Task<ApiResult> DeleteByBlogId(int blogId)
    {
        var comments = context.Comments.Where(x => x.BlogId == blogId);
        context.Comments.RemoveRange(comments);
        await context.SaveChangesAsync();

        return ApiResult.Success();
    }
    
    
}