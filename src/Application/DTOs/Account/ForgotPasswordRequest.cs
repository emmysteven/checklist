using System.ComponentModel.DataAnnotations;

namespace Checklist.Application.DTOs.Account;

public class ForgotPasswordRequest
{
    [Required] [EmailAddress] public string? Email { get; set; }
}