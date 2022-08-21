using Checklist.Domain.Common;
using Checklist.Domain.Entities;

namespace Checklist.Domain.Events;

public class TodoDeletedEvent: DomainEvent
{
    public TodoDeletedEvent(Todo todo)
    {
        Todo = todo;
    }

    public Todo Todo { get; }
}