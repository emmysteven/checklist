using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Todos.Queries;

public class GetTodoByIdQuery : IRequest<Todo>
{
    public GetTodoByIdQuery(int id)
    {
        Id = id;
    }
    public int Id { get; }
} 
    

public class GetTodoByIdHandler : IRequestHandler<GetTodoByIdQuery, Todo>
{
    private readonly ITodoRepository _repo;

    public GetTodoByIdHandler(ITodoRepository repo)
    {
        _repo = repo;
    }

    public async Task<Todo> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        var todo = await _repo.GetByIdAsync(request.Id);
        if (todo == null) throw new ApiException($"Todo Not Found.");
        
        return todo;
    }
}