using Checklist.Application.Common.Interfaces;

namespace Checklist.WebUI.Services;

public class CurrentUserService: ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?.User.FindFirst("Id")?.Value!;
        Console.WriteLine($"UserId is: {UserId}");
    }
    public string UserId { get; }
    public bool IsAuthenticated => UserId != null;
}