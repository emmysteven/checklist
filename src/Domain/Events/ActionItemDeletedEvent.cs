using Checklist.Domain.Common;
using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class ActionItemDeletedEvent: DomainEvent
{
    public ActionItemDeletedEvent(Check check)
    {
        Check = check;
    }

    public Check Check { get; }
}