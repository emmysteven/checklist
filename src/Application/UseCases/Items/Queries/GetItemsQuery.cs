using AutoMapper;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using MediatR;

namespace Checklist.Application.UseCases.Items.Queries;

public record GetItemsQuery : IRequest<PaginatedResponse<IEnumerable<ItemVm>>>
{
    public int TodoId { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; } 
}

public class GetItemsHandler : IRequestHandler<GetItemsQuery, PaginatedResponse<IEnumerable<ItemVm>>>
{
    private readonly IItemRepository _repo;
    private readonly IMapper _mapper;

    public GetItemsHandler(IItemRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    
    
    public async Task<PaginatedResponse<IEnumerable<ItemVm>>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<ItemParameter>(request);
        var item = await _repo.GetPagedResponseAsync(filter.PageNumber, filter.PageSize);
        var itemVm = _mapper.Map<IEnumerable<ItemVm>>(item);
        return new PaginatedResponse<IEnumerable<ItemVm>>(itemVm, filter.PageNumber, filter.PageSize); 
    }
}