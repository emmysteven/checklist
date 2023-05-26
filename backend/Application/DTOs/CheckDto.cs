using Checklist.Domain.Entities;

namespace Checklist.Application.DTOs;

public class CheckDto
{
    public IEnumerable<Check> Checks { get; set; } = new List<Check>();
}