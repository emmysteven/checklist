using Checklist.Domain.Common;
using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class TodoCompletedEvent: DomainEvent
{
    public TodoCompletedEvent(Todo todo)
    {
        Todo = todo;
    }

    public Todo Todo { get; }
}