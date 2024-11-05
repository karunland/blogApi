using System.Text;
using System.Xml.Serialization;
using BlogApi.Application.Interfaces;
using BlogApi.Application.Services;
using BlogApi.Infrastructure.Persistence;
using BlogApi.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace BlogApi;

public static class ProgramExtensions
{
    public static IServiceCollection AddStartupServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHttpContextAccessor();
        
        var connectionString = configuration.GetConnectionString("BlogConnection");
        services.AddDbContext<BlogContext>(options => options.UseNpgsql(connectionString));
        
        services.AddScoped<BlogRepo>();
        services.AddScoped<UserRepo>();
        services.AddScoped<CategoryRepo>();
        services.AddScoped<CommentRepo>();
        services.AddSingleton<FileRepo>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        services.AddSwaggerGen(swagger =>
        {
            // swagger.EnableAnnotations();
            swagger.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Blog API",
                    Description = ""
                });
            swagger.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "ONLY TOKEN"
                });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    Array.Empty<string>()
                }
            });
        });

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Basesettings:JwtSecret"]!)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        
        services.AddCors(options => options.AddPolicy("AllowAllOrigins",
            builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed(_ => true)));

        return services;
    }

    public static async Task<IApplicationBuilder> UseAppServicesAsync(this IApplicationBuilder app,
        IConfiguration configuration,
        IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var blogContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
        await SeedData.SeedDatabaseAsync(blogContext);

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }
}
