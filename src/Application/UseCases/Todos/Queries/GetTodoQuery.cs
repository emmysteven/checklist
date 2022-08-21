using AutoMapper;
using AutoMapper.QueryableExtensions;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using Checklist.Application.Mappings;
using MediatR;

namespace Checklist.Application.UseCases.Todos.Queries;

public record GetTodoQuery : IRequest<PaginatedResponse<TodoVm>>
{
    public int Id { get; init; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
    
public class GetAllTodoHandler : IRequestHandler<GetTodoQuery, PaginatedResponse<TodoVm>>
{
    private readonly IDataContext _context;
    private readonly IMapper _mapper;

    public GetAllTodoHandler(IDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<TodoVm>> Handle(GetTodoQuery request, CancellationToken cancellationToken)
    {
        return await _context.Todos
            .Where(x => x.Id == request.Id)
            .OrderBy(x => x.Id)
            .ProjectTo<TodoVm>(_mapper.ConfigurationProvider)
            .PaginatedResponseAsync(request.PageNumber, request.PageSize);
        // var filter = _mapper.Map<GetTodoParameter>(request);
        // var todo = await _context.Todos.FindAsync(filter.PageNumber, filter.PageSize);
        // var todoViewModel = _mapper.Map<IEnumerable<TodoVm>>(todo);
        // return new PaginateResponse<IEnumerable<TodoVm>>(todoViewModel, filter.PageNumber, filter.PageSize); 
    }
}