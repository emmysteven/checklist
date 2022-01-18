using Checklist.Domain.Common;
using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class TodoListCompletedEvent: DomainEvent
{
    public TodoListCompletedEvent(Todo todo)
    {
        Todo = todo;
    }

    public Todo Todo { get; }
}