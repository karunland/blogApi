using BlogApi.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

public class BlogController(BlogRepo blogRepo) : BaseApiController
{
    
}