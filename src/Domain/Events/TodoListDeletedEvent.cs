using Checklist.Domain.Common;
using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class StepListDeletedEvent: DomainEvent
{
    public StepListDeletedEvent(Todo todo)
    {
        Todo = todo;
    }

    public Todo Todo { get; }
}