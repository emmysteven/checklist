using Checklist.Domain.Common;

namespace Checklist.Domain.Entities;

public class Item : AuditableEntity
{
    public int TodoId { get; set; }
    public Todo Todo { get; set; } = null!;
    public string StartTime { get; set; } = String.Empty;
    public string EndTime { get; set; } = String.Empty;
    
    // private bool _done;
    // public bool Done
    // {
    //     get => _done;
    //     set
    //     {
    //         if (value == true && _done == false)
    //         {
    //             AddDomainEvent(new ItemCompletedEvent(this));
    //         }
    //
    //         _done = value;
    //     }
    // }
}