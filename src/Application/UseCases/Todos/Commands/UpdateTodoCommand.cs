using AutoMapper;
using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Todos.Commands;

public class UpdateTodoCommand : IRequest<Todo>
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
    
public class UpdateTodoHandler : IRequestHandler<UpdateTodoCommand, Todo>
{
    private readonly IMapper _mapper;
    private readonly ITodoRepository _todoRepository;

    public UpdateTodoHandler(ITodoRepository todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    public async Task<Todo> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var isTodo = await _todoRepository.GetByIdAsync(request.Id);
        if (isTodo == null) throw new ApiException("Todo Not Found.");
        var todo = _mapper.Map<Todo>(request);

        return await _todoRepository.UpdateAsync(todo);
    }
}