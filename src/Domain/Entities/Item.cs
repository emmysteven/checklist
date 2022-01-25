using Checklist.Domain.Common;

namespace Checklist.Domain.Entities;

public class Item : AuditableEntity
{
    public string? Name { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    // public int ListId { get; set; }
    public Todo todo { get; set; } = null!;
}