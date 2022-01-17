using Checklist.Domain.Common;
using Checklist.Domain.Events;

namespace Checklist.Domain.Entities;

public class Shop: AuditableEntity, IHasDomainEvent
{
    public string Process { get; set; }
    
    public int ListId { get; set; }
    private bool _done;
    public bool Done
    {
        get => _done;
        set
        {
            if (value && _done == false)
            {
                DomainEvents.Add(new ItemListCompletedEvent(this));
            }
    
            _done = value;
        }
    }
    
    public ItemList ItemList { get; set; } = null!;

    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}