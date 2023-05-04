using Checklist.Application.Common.Interfaces;

namespace Checklist.WebUI.Services;

public class CurrentUserService: ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        Username = httpContextAccessor.HttpContext?.User.FindFirst("Username")?.Value!;
        Console.WriteLine($"Username is: {Username}");
    }
    public string Username { get; }
    public bool IsAuthenticated => true;
}