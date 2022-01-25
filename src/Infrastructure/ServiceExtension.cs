using System.Text;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Settings;
using Checklist.Infrastructure.Contexts;
using Checklist.Infrastructure.Repositories;
using Checklist.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

// using Hangfire;

namespace Checklist.Infrastructure;

public static class ServiceExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        // services.AddDbContext<DataContext>(x =>
        //     x.UseSqlServer(config.GetConnectionString("db")));
            
        services.AddDbContext<DataContext>(x =>
            x.UseSqlite(config.GetConnectionString("DevDB")));

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = config.GetConnectionString("RedisUrl");
        });

        services.AddTransient<ICacheService, CacheService>();

        // services.AddHangfire(x => x.UseSqlServerStorage(config.GetConnectionString("db")));
        // services.AddHangfireServer();

        services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddTransient<ITodoRepository, TodoRepository>();
        services.AddTransient<IItemRepository, ItemRepository>();
        services.AddTransient<IUserRepository, UserRepository>();

        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IDateService, DateService>();

        services.Configure<JWTSettings>(config.GetSection("JWTSettings"));
        services.Configure<MailSettings>(config.GetSection("MailSettings"));

        services.AddSingleton(x => x.GetRequiredService<IOptions<JWTSettings>>().Value);
        services.AddSingleton(x => x.GetRequiredService<IOptions<MailSettings>>().Value);

        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = config["JWTSettings:Issuer"],

                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = config["JWTSettings:Audience"],

                    // Validate the token expiry
                    ValidateLifetime = true,

                    // If you want to allow a certain amount of clock drift, set that here:
                    ClockSkew = TimeSpan.Zero,

                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTSettings:Key"]))
                };
                
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["X-Access-Token"];
                        return Task.CompletedTask;
                    }
                };
            });
    }
}
