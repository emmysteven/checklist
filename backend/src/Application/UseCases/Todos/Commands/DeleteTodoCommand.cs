using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Todos.Commands;
public record DeleteTodoCommand(int Id) : IRequest<Todo>;
public class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand, Todo>
{
    private readonly ITodoRepository _repo;

    public DeleteTodoHandler(ITodoRepository repo)
    {
        _repo = repo;
    }
    
    public async Task<Todo> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _repo.GetByIdAsync(request.Id);
        if (todo == null) throw new ApiException("Todo Not Found.");
        
        return await _repo.DeleteAsync(todo);
    }
}