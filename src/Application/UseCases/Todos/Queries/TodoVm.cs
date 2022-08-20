using Checklist.Application.Mappings;
using Checklist.Application.UseCases.Items.Queries;
using Checklist.Domain.Entities;

namespace Checklist.Application.UseCases.Todos.Queries;

public class TodoVm : IMapFrom<Todo>
{
    public TodoVm()
    {
        Items = new List<ItemVm>();
    }

    public int Id { get; set; }

    public string? Name { get; set; }

    public IList<ItemVm> Items { get; set; }
}