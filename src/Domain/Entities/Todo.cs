using Checklist.Domain.Common;

namespace Checklist.Domain.Entities;

public class Todo: AuditableEntity//, IHasDomainEvent
{
    public string Name { get; set; }
    
    // public int ListId { get; set; }
    // private bool _done;
    // public bool Done
    // {
    //     get => _done;
    //     set
    //     {
    //         if (value && _done == false)
    //         {
    //             DomainEvents.Add(new TodoListCompletedEvent(this));
    //         }
    //
    //         _done = value;
    //     }
    // }
    public IList<Item> items { get; private set; } = new List<Item>();


    // public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}