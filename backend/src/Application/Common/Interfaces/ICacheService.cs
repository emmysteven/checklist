namespace Checklist.Application.Common.Interfaces;

public interface ICacheService
{
    Task<object?> GetAsync(string key);
    Task SetAsync(string key, object value, TimeSpan expirationTimeFromNow);
    // public Task RefreshAsync(string key);
    Task RemoveAsync(string? key);
}