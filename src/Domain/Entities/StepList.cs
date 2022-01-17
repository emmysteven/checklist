using Checklist.Domain.Common;

namespace Checklist.Domain.Entities;

public class ItemList : AuditableEntity
{
    public IList<Step> Items { get; private set; } = new List<Step>();
}