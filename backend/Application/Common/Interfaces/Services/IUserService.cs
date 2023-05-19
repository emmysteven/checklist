using Checklist.Application.Common.Wrappers;
using Checklist.Application.DTOs.Account;

namespace Checklist.Application.Common.Interfaces.Services;

public interface IUserService
{
    Task<Response<AuthResponse>> GetByIdAsync(int id);
    Task<IEnumerable<AuthResponse>> GetAllAsync();
    Task<Response<string?>> RegisterAsync(RegisterRequest request, string? origin);
    Task<AuthResponse> AuthenticateAsync(AuthRequest request, string ipAddress);
    Task<Response<string?>> VerifyEmailAsync(int id, string token);
    Task ForgotPasswordAsync(ForgotPasswordRequest request, string? origin);
    Task<Response<string?>> ResetPasswordAsync(ResetPasswordRequest request);
}