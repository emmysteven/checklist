using AutoMapper;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Todos.Commands;
public class CreateTodoCommand : IRequest<Response<int>>
{
    public string? Name { get; set; }
}
    
public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, Response<int>>
{
    private readonly IMapper _mapper;
    private readonly ITodoRepository _todoRepository;

    public CreateTodoHandler(IMapper mapper, ITodoRepository todoRepository)
    {
        _mapper = mapper;
        _todoRepository = todoRepository;
    }

    public async Task<Response<int>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = _mapper.Map<Todo>(request);
        await _todoRepository.CreateAsync(todo);
        return new Response<int>(todo.Id);
    }
}