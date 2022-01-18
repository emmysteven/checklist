using Checklist.Domain.Common;
using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class StepListCompletedEvent: DomainEvent
{
    public StepListCompletedEvent(Todo todo)
    {
        Todo = todo;
    }

    public Todo Todo { get; }
}