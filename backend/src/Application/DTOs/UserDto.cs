using Checklist.Domain.Enums;

namespace Checklist.Application.DTOs;

public class UserDto
{
    public string? UserName { get; set; }
    public Roles? Role { get; set; }
}