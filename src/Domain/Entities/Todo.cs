using Checklist.Domain.Common;
using Checklist.Domain.Enums;

namespace Checklist.Domain.Entities;

public class Todo: AuditEntity//, IHasDomainEvent
{
    public string Name { get; set; }
    public Groups Group { get; set; }

    public Todo(string name, Groups group)
    {
        Name = name;
        Group = group;
    }

    // public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}