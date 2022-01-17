using Checklist.Domain.Common;
using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class ActionItemDeletedEvent: DomainEvent
{
    public ActionItemDeletedEvent(Item item)
    {
        Item = item;
    }

    public Item Item { get; }
}