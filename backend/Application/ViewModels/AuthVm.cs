using System.Text.Json.Serialization;

namespace Checklist.Application.ViewModels;

public class AuthResponse
{
    public string? FullName { get; set; }
    public string? ResponseCode { get; set; }
    public string? ResponseMessage { get; set; }
    public string? Token { get; set; }
    [JsonIgnore] public string? RefreshToken { get; set; }
}