using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class ItemCreatedEvent
{
    public ItemCreatedEvent(Item item)
    {
        Item = item;
    }

    public Item Item { get; }
}