using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogApi.Application.Common.Settings;
using BlogApi.Application.DTOs.User;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace BlogApi.Application.Helper;


public class TokenHelper(BaseSettings baseSettings, IHttpContextAccessor context)
{

    public string GenerateToken(JwtTokenDto user)
    {
        var jwtSecret = baseSettings.JwtSecret;

        JwtSecurityTokenHandler tokenHandler = new();
        var key = Encoding.ASCII.GetBytes(jwtSecret);
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new("LoggedInUser", user.ToJson().ToCryptoText())
            }),
            Expires = DateTime.Now.AddDays(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}

