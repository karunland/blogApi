using System.Net;
using BlogApi.Application.Common.Messages;
using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.User;
using BlogApi.Application.Helper;
using BlogApi.Application.Interfaces;
using BlogApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.Persistence.Repositories;

public class UserRepo(BlogContext context, ICurrentUserService currentUserService)
{
    public async Task<ApiResult> Register(UserAddDto user)
    {
        if (await context.Users.AnyAsync(x => x.Email == user.Email))
        {
            return ApiError.Failure(Messages.AlreadyExist, HttpStatusCode.BadRequest);
        }
        
        var newUser = new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.UserName,
            Email = user.Email,
            Password = user.Password.ToSha1(),
        };

        await context.Users.AddAsync(newUser);
        await context.SaveChangesAsync();

        return ApiResult.Success();
    }
    
    public async Task<ApiResult<MeDto>> Login(UserLoginDto input)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == input.Email && x.Password == input.Password.ToSha1());
        if (user == null)
        {
            return ApiError.Failure(Messages.NotFound, HttpStatusCode.NotFound);
        }
        
        return new MeDto
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.Username,
            Token = TokenHelper.GenerateToken(new JwtTokenDto()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id
            })
        };
    }
    
    public async Task<ApiResult<UserDto>> Me()
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