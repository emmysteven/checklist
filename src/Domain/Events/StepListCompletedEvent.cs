using Checklist.Domain.Common;
using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class StepListCompletedEvent: DomainEvent
{
    public StepListCompletedEvent(Step step)
    {
        Step = step;
    }

    public Step Step { get; }
}