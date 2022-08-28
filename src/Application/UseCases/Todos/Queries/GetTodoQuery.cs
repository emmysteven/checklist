using AutoMapper;
using AutoMapper.QueryableExtensions;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using Checklist.Application.Mappings;
using MediatR;

namespace Checklist.Application.UseCases.Todos.Queries;

public record GetTodoQuery : IRequest<PaginatedResponse<IEnumerable<TodoVm>>>
{
    public int Id { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}
    
public class GetAllTodoHandler : IRequestHandler<GetTodoQuery, PaginatedResponse<IEnumerable<TodoVm>>>
{
    private readonly ITodoRepository _repo;
    private readonly IMapper _mapper;

    public GetAllTodoHandler(ITodoRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<IEnumerable<TodoVm>>>  Handle(GetTodoQuery request, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<TodoParameter>(request);
        var todo = await _repo.GetPagedResponseAsync(filter.PageNumber, filter.PageSize);
        var todoVm = _mapper.Map<IEnumerable<TodoVm>>(todo);
        
        return new PaginatedResponse<IEnumerable<TodoVm>>(todoVm, filter.PageNumber, filter.PageSize); 
    }
}