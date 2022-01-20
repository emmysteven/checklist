using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Todos.Commands;

public class DeleteTodoCommand : IRequest<Todo>
{
    public DeleteTodoCommand(Todo todo)
    {
        Todo = todo;
    }

    public Todo Todo { get; }
}
    
public class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand, Todo>
{
    private readonly ITodoRepository _todoRepository;

    public DeleteTodoHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<Todo> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        return await _todoRepository.DeleteAsync(request.Todo);
    }
}