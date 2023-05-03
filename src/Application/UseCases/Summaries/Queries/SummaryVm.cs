using Checklist.Application.Mappings;
using Checklist.Domain.Entities;

namespace Checklist.Application.UseCases.Summaries.Queries;

public class SummaryVm : IMapFrom<Summary>
{
    public int Id { get; set; }
    public string Makers { get; set; } = string.Empty;
    public string Checkers { get; set; } = string.Empty;
    public string Dbas { get; set; } = string.Empty;
    public DateTime? EodDate { get; set; }
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public int? TxnCount { get; set; } 
}