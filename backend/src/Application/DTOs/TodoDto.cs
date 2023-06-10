using Checklist.Domain.Enums;

namespace Checklist.Application.DTOs;

public record TodoDto
{
    public string? TodoName { get; set; }
    public Groups TodoGroup { get; set; }
}