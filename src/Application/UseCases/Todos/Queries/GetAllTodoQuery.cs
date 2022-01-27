using AutoMapper;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using MediatR;

namespace Checklist.Application.UseCases.Todos.Queries;

public class GetAllTodoQuery : IRequest<PagedResponse<IEnumerable<GetAllTodoVm>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
    
public class GetAllTodoHandler : IRequestHandler<GetAllTodoQuery, PagedResponse<IEnumerable<GetAllTodoVm>>>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;

    public GetAllTodoHandler(ITodoRepository todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    public async Task<PagedResponse<IEnumerable<GetAllTodoVm>>> Handle(GetAllTodoQuery request, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<GetAllTodoParameter>(request);
        var todo = await _todoRepository.GetPagedResponseAsync(filter.PageNumber, filter.PageSize);
        var todoViewModel = _mapper.Map<IEnumerable<GetAllTodoVm>>(todo);
        return new PagedResponse<IEnumerable<GetAllTodoVm>>(todoViewModel, filter.PageNumber, filter.PageSize); 
    }
}