namespace Checklist.Application.DTOs.Account;

public class AuthRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}