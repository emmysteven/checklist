using System.Security.Claims;
using Checklist.Application.Common.Interfaces.Services;

namespace Checklist.Infrastructure.Services;

public class CurrentUserService: ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        Username = httpContextAccessor.HttpContext?.User.FindFirstValue("Username");
        Console.WriteLine($"Username is: { Username }");
    }
    public string? Username { get; }
}