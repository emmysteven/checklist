using Checklist.Domain.Common;

namespace Checklist.Domain.Entities;

public class Item : AuditableEntity
{
    public new int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
    public DateTime? EodDate { get; set; }
    
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