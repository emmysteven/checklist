using AutoMapper;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using MediatR;

namespace Checklist.Application.UseCases.Todos.Queries;

public record GetTodoQuery : IRequest<Response<IEnumerable<TodoVm>>>
{
    public int Id { get; init; }
}
    
public class GetAllTodoHandler : IRequestHandler<GetTodoQuery, Response<IEnumerable<TodoVm>>>
{
    private readonly ITodoRepository _repo;
    private readonly IMapper _mapper;

    public GetAllTodoHandler(ITodoRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<TodoVm>>>  Handle(GetTodoQuery request, CancellationToken cancellationToken)
    {
        var todo = await _repo.GetAllAsync();
        var todoVm = _mapper.Map<IEnumerable<TodoVm>>(todo);
        
        return new Response<IEnumerable<TodoVm>>(todoVm); 
    }
}