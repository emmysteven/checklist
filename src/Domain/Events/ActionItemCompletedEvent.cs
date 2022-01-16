using Checklist.Domain.Common;
using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class ActionItemCompletedEvent: DomainEvent
{
    public ActionItemCompletedEvent(Check check)
    {
        Check = check;
    }

    public Check Check { get; }
}