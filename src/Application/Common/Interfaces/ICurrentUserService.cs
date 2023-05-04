namespace Checklist.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string Username { get; }
    bool IsAuthenticated { get; }
}