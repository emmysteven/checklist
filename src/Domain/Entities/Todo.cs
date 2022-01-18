using Checklist.Domain.Common;
using Checklist.Domain.Events;

namespace Checklist.Domain.Entities;

public class Step: AuditableEntity, IHasDomainEvent
{
    public string Name { get; set; }
    
    public int ListId { get; set; }
    private bool _done;
    public bool Done
    {
        get => _done;
        set
        {
            if (value && _done == false)
            {
                DomainEvents.Add(new StepListCompletedEvent(this));
            }
    
            _done = value;
        }
    }
    
    public StepList StepList { get; set; } = null!;

    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}