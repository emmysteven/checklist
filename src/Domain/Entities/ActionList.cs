using Checklist.Domain.Common;

namespace Checklist.Domain.Entities;

public class ActionList : AuditableEntity
{
    public int Id { get; set; }

    //public string Description { get; set; }

    //public Colour Colour { get; set; } = Colour.White;

    public IList<Check> Items { get; private set; } = new List<Check>();
}