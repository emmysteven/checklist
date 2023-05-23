using Checklist.Domain.Common;
using Checklist.Domain.Enums;

namespace Checklist.Domain.Entities;

public class Todo: AuditEntity
{
    public string TodoName { get; set; } = string.Empty;
    public Groups TodoGroup { get; set; }
}