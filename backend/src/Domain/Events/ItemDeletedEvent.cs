using Checklist.Domain.Common;
using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class ItemDeletedEvent: DomainEvent
{
    public ItemDeletedEvent(Item item)
    {
        Item = item;
    }

    public Item Item { get; }
}