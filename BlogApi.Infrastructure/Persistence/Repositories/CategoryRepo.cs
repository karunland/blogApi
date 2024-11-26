using BlogApi.Application.Common.Messages;
using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Category;
using BlogApi.Core.Entities;

namespace BlogApi.Infrastructure.Persistence.Repositories;

public partial class CategoryRepo
{
}

public partial class CategoryRepo(BlogContext context)
{
    public async Task<ApiResult> Create(CategoryAddDto category)
    {
        var newCategory = new Category
        {
            Name = category.Name,
            CreatedAt = DateTime.UtcNow
        };

        await context.Categories.AddAsync(newCategory);
        await context.SaveChangesAsync();

        return ApiResult.Success();
    }

    public async Task<ApiResultPagination<CategoriesDto>> GetAll(FilterModel filter)
    {
        var categories = context.Categories
            .Select(x => new CategoriesDto
            {
                Id = x.Id,
                Name = x.Name,
                CreatedAt = x.CreatedAt
            });

        return await categories.PaginatedListAsync(filter.PageNumber, filter.PageSize);
    }

    public async Task<ApiResult> Delete(int id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null)
        {
            return ApiError.Failure(Messages.NotFound);
        }

        context.Categories.Remove(category);
        await context.SaveChangesAsync();

        return ApiResult.Success();
    }
}