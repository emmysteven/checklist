using AutoMapper;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using MediatR;

namespace Checklist.Application.UseCases.Todos.Queries;

public class GetAllTodoQuery : IRequest<PaginateResponse<IEnumerable<GetAllTodoVm>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
    
public class GetAllTodoHandler : IRequestHandler<GetAllTodoQuery, PaginateResponse<IEnumerable<GetAllTodoVm>>>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;

    public GetAllTodoHandler(ITodoRepository todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    public async Task<PaginateResponse<IEnumerable<GetAllTodoVm>>> Handle(GetAllTodoQuery request, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<GetAllTodoParameter>(request);
        var todo = await _todoRepository.GetPagedResponseAsync(filter.PageNumber, filter.PageSize);
        var todoViewModel = _mapper.Map<IEnumerable<GetAllTodoVm>>(todo);
        return new PaginateResponse<IEnumerable<GetAllTodoVm>>(todoViewModel, filter.PageNumber, filter.PageSize); 
    }
}