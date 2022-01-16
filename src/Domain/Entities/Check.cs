using Checklist.Domain.Common;

namespace Checklist.Domain.Entities;

public class Check: AuditableEntity //, IHasDomainEvent
{
    public int Id { get; set; }

    // public int ListId { get; set; }

    // public string? Title { get; set; }

    public string? Item { get; set; }

    // public PriorityLevel Priority { get; set; }
    // public DateTime? Reminder { get; set; }

    // private bool _done;
    // public bool Done
    // {
    //     get => _done;
    //     set
    //     {
    //         if (value && _done == false)
    //         {
    //             DomainEvents.Add(new ActionItemCompletedEvent(this));
    //         }
    //
    //         _done = value;
    //     }
    // }
    //
    // public ActionList List { get; set; } = null!;

    // public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}