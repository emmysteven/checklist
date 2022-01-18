using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class StepListCreatedEvent
{
    public StepListCreatedEvent(Step step)
    {
        Step = step;
    }

    public Step Step { get; }
}