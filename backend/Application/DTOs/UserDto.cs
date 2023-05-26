using System.ComponentModel.DataAnnotations;
using Checklist.Domain.Enums;

namespace Checklist.Application.DTOs;

public class RegisterDto
{
    [Required] public string? FullName { get; set; }

    [Required] public string? UserName { get; set; }

    public Roles Role { get; set; }

}