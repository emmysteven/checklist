using Checklist.Application.Mappings;
using Checklist.Domain.Entities;

namespace Checklist.Application.ViewModels;

public class SummaryVm : IMapFrom<Summary>
{
    public long Id { get; set; }
    public string Makers { get; set; } = string.Empty;
    public string Checkers { get; set; } = string.Empty;
    public string Dbas { get; set; } = string.Empty;
    public DateTime? EodDate { get; set; }
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public int? TxnCount { get; set; } 
}
