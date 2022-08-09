using Checklist.Domain.Common;

namespace Checklist.Domain.Entities;

public class Item : AuditableEntity
{
    public int? TodoId { get; set; }
    public Todo Todos { get; set; } = null!;
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
}