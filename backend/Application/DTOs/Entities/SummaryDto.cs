namespace Checklist.Application.DTOs.Entities;

public class SummaryDto
{
    public string? Makers { get; set; }
    public string? Checkers { get; set; }
    public string? Dbas { get; set; }
    public DateTime? EodDate { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public string? Duration { get; set; }
    public int? TxnCount { get; set; }
}