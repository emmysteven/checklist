using AutoMapper;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Todos.Commands;
public class CreateTodoCommand : IRequest<Response<int>>
{
    public string Name { get; set; } = string.Empty;
}
    
public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, Response<int>>
{
    private readonly ITodoRepository _repo;
    private readonly IMapper _mapper;

    public CreateTodoHandler(ITodoRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Response<int>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = _mapper.Map<Todo>(request);
        await _repo.CreateAsync(todo);
        
        return new Response<int>(todo.Id);
    }
}