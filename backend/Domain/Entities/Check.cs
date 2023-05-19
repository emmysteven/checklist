using Checklist.Domain.Common;

namespace Checklist.Domain.Entities;

public class Check : AuditEntity
{
    public new int Id { get; set; }
    public string CheckName { get; set; } = string.Empty;
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
    public DateTime? EodDate { get; set; }
    public string Remark { get; set; } = string.Empty;
}