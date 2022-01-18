using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class TodoListCreatedEvent
{
    public TodoListCreatedEvent(Todo todo)
    {
        Todo = todo;
    }

    public Todo Todo { get; }
}