using Checklist.Domain.Common;

namespace Checklist.Domain.Entities;

public class StepList : AuditableEntity
{
    public IList<Todo> Names { get; private set; } = new List<Todo>();
}