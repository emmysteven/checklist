using Checklist.Domain.Common;
using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class StepListDeletedEvent: DomainEvent
{
    public StepListDeletedEvent(Step step)
    {
        Step = step;
    }

    public Step Step { get; }
}