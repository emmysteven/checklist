using Checklist.Domain.Common;
using Checklist.Domain.Enums;

namespace Checklist.Domain.Entities;

public class User : AuditEntity
{
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public Roles? Role { get; set; }
}