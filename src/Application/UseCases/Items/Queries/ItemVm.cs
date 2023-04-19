using System.Runtime.CompilerServices;
using Checklist.Application.Mappings;
using Checklist.Domain.Entities;

namespace Checklist.Application.UseCases.Items.Queries;

public class ItemVm : IMapFrom<Item>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
    public DateTime? EodDate { get; set; }
}