using System.Net;
using BlogApi.Application.Common.Messages;
using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.User;
using BlogApi.Application.Helper;
using BlogApi.Application.Interfaces;
using BlogApi.Application.Services;
using BlogApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.Persistence.Repositories;

public class UserRepo(BlogContext context, ICurrentUserService currentUserService, TokenHelper tokenService)
{
    public async Task<ApiResult> Register(UserAddDto user)
    {
        var newUser = new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.UserName,
            Email = user.Email,
            Password = user.Password.ToSha1()
        };

        await context.Users.AddAsync(newUser);
        await context.SaveChangesAsync();

        return ApiResult.Success();
    }
    
    public async Task<ApiResult<MeDto>> Login(UserLoginDto user)
    {
        var User = await context.Users.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password.ToSha1());
        if (User == null)
        {
            return ApiError.Failure(Messages.NotFound, HttpStatusCode.NotFound);
        }
        
        return new MeDto
        {
            Email = User.Email,
            FirstName = User.FirstName,
            LastName = User.LastName,
            UserName = User.Username,
            Token = tokenService.GenerateToken(new JwtTokenDto()
            {
                Email = User.Email,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Id = User.Id
            })
        };
    }
    
    public async Task<ApiResult<UserDto>> me()
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == currentUserService.Id);
        
        if (user == null)
        {
            return ApiError.Failure(Messages.NotFound);
        }
        
        return new UserDto
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.Username
        };
    }
}