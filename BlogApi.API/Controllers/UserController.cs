using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.User;
using BlogApi.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

public class UserController(UserRepo userRepo) : BaseApiController
{
    // register login
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ApiResult> Register(UserAddDto user)
    {
        var result = await userRepo.Register(user);
        return result;
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ApiResult<MeDto>> Login(UserLoginDto user)
    {
        return await userRepo.Login(user);
    }
    
    [HttpGet("me")]
    public async Task<ApiResult<UserDto>> me()
    {
        return await userRepo.me();
    }
    
}