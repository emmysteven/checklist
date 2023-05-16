using AutoMapper;
using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Todos.Commands;

public record UpdateTodoCommand : IRequest<Todo>
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
    
public class UpdateTodoHandler : IRequestHandler<UpdateTodoCommand, Todo>
{
    private readonly ITodoRepository _repo;
    private readonly IMapper _mapper;

    public UpdateTodoHandler(ITodoRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Todo> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var isTodo = await _repo.GetByIdAsync(request.Id);
        if (isTodo == null) throw new ApiException("Todo Not Found.");
        
        var todo = _mapper.Map<Todo>(request);
        
        return await _repo.UpdateAsync(todo);
    }
}