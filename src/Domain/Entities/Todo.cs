using Checklist.Domain.Common;
using Checklist.Domain.Enums;

namespace Checklist.Domain.Entities;

public class Todo: AuditEntity//, IHasDomainEvent
{
    public string TodoName { get; set; }
    public Groups TodoGroup { get; set; }

    public Todo(string todoName, Groups todoGroup)
    {
        TodoName = todoName;
        TodoGroup = todoGroup;
    }

    // public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}