using Checklist.Domain.Common;
using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class ItemCompletedEvent: DomainEvent
{
    public ItemCompletedEvent(Item item)
    {
        Item = item;
    }

    public Item Item { get; }
}