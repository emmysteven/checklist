using AutoMapper;
using AutoMapper.QueryableExtensions;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using Checklist.Application.Mappings;
using MediatR;

namespace Checklist.Application.UseCases.Items.Queries;

public record GetItemsQuery : IRequest<PaginateResponse<ItemVm>>
{
    public int TodoId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetItemsHandler : IRequestHandler<GetItemsQuery, PaginateResponse<ItemVm>>
{
    private readonly IDataContext _context;
    private readonly IMapper _mapper;

    public GetItemsHandler(IDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginateResponse<ItemVm>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Items
            .Where(x => x.TodoId == request.TodoId)
            .OrderBy(x => x.TodoId)
            .ProjectTo<ItemVm>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}