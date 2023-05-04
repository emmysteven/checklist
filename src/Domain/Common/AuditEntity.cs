namespace Checklist.Domain.Common;

public class AuditEntity
{
    public int Id { get; set; }
    public string MakerId { get; set; } = string.Empty;
    public DateTime? MakerDt { get; set; }
    public string CheckerId { get; set; } = string.Empty;
    public DateTime? CheckerDt { get; set; }
    public bool? AuthStatus { get; set; }
}