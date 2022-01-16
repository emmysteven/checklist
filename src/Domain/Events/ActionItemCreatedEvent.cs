using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class ActionItemCreatedEvent
{
    public ActionItemCreatedEvent(Check check)
    {
        Check = check;
    }

    public Check Check { get; }
}