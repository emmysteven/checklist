using Checklist.Domain.Common;

namespace Checklist.Domain.Entities;

public class Todo: AuditableEntity//, IHasDomainEvent
{
    public string Name { get; set; } = string.Empty;
    public IList<Item> Items { get; } = new List<Item>();

    // public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}