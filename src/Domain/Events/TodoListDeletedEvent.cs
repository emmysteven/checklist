using Checklist.Domain.Common;
using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class TodoListDeletedEvent: DomainEvent
{
    public TodoListDeletedEvent(Todo todo)
    {
        Todo = todo;
    }

    public Todo Todo { get; }
}