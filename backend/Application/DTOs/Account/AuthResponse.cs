using System.Text.Json.Serialization;

namespace Checklist.Application.DTOs.Account;

public class AuthResponse
{
    public string? Token { get; set; }
    [JsonIgnore] public string? RefreshToken { get; set; }
}