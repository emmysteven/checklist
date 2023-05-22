using System.Reflection;
using System.Text;
using Checklist.Application.Common.Interfaces.Repositories;
using Checklist.Application.Common.Interfaces.Services;
using Checklist.Application.Settings;
using Checklist.Infrastructure.Repositories;
using Checklist.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Checklist.Infrastructure;

public static class ServiceExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
    {

        services.AddDbContext<DataContext>(x =>
        {
            x.UseSqlServer(config.GetConnectionString(env.IsDevelopment() ? "Dev" : "Prod"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
        });

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = config.GetConnectionString("RedisUrl");
        });
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddTransient<ICacheService, CacheService>();

        services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddTransient<ITodoRepository, TodoRepository>();
        services.AddTransient<ICheckRepository, CheckRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ISummaryRepository, SummaryRepository>();

        services.AddTransient<IDateService, DateService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ITodoService, TodoService>();
        services.AddTransient<ICheckService, CheckService>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<ISummaryService, SummaryService>();
        
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

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
