namespace Checklist.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string UserId { get; }
    bool IsAuthenticated { get; }
}