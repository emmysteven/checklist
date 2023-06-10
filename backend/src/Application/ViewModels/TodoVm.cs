using Checklist.Application.Mappings;
using Checklist.Domain.Entities;

namespace Checklist.Application.ViewModels;

public class TodoVm : IMapFrom<Todo>
{
    public long Id { get; set; }
    public string? TodoName { get; set; }
    public string? TodoGroup { get; set; }
}