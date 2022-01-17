using Checklist.Domain.Common;
using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class ActionItemCompletedEvent: DomainEvent
{
    public ActionItemCompletedEvent(Item item)
    {
        Item = item;
    }

    public Item Item { get; }
}