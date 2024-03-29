using Checklist.Application.Mappings;
using Checklist.Domain.Entities;

namespace Checklist.Application.ViewModels;

public class CheckVm : IMapFrom<Check>
{
    public long Id { get; set; }
    public string CheckName { get; set; } = string.Empty;
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
    public DateTime? EodDate { get; set; }
    public bool? AuthStatus { get; set; } = false;
}

