using Checklist.Application.Mappings;
using Checklist.Application.UseCases.Items.Queries;
using Checklist.Domain.Entities;

namespace Checklist.Application.UseCases.Todos.Queries;

public class TodoVm : IMapFrom<Todo>
{
    public int Id { get; set; }
    public string? TodoName { get; set; }
    public string? TodoGroup { get; set; }
}