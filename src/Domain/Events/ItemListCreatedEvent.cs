using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class ActionItemCreatedEvent
{
    public ActionItemCreatedEvent(Item item)
    {
        Item = item;
    }

    public Item Item { get; }
}