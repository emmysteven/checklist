using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class StepListCreatedEvent
{
    public StepListCreatedEvent(Todo todo)
    {
        Todo = todo;
    }

    public Todo Todo { get; }
}