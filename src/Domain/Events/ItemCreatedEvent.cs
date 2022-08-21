using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class TodoCreatedEvent
{
    public TodoCreatedEvent(Todo todo)
    {
        Todo = todo;
    }

    public Todo Todo { get; }
}