using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Blog;
using BlogApi.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

public class BlogController(BlogRepo blogRepo) : BaseApiController
{
    [HttpPost]
    public async Task<ApiResult> Create(BlogAddDto blog)
    {
        return await blogRepo.Create(blog);
    }
    
    [HttpPost]
    public async Task<ApiResult> Update(BlogUpdateDto blog)
    {
        return await blogRepo.Update(blog);
    }
    
    [HttpPost("{id}")]
    public async Task<ApiResult> Delete(int id)
    {
        return await blogRepo.Delete(id);
    }
    
    [HttpGet]
    public async Task<ApiResultPagination<BlogsDto>> GetAll()
    {
        return await blogRepo.GetAll();
    }
    
}