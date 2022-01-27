using AutoMapper;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using MediatR;

namespace Checklist.Application.UseCases.Items.Queries;

public class GetAllItemQuery : IRequest<PagedResponse<IEnumerable<GetAllItemVm>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
    
public class GetAllItemHandler : IRequestHandler<GetAllItemQuery, PagedResponse<IEnumerable<GetAllItemVm>>>
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;

    public GetAllItemHandler(IItemRepository itemRepository, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
    }

    public async Task<PagedResponse<IEnumerable<GetAllItemVm>>> Handle(GetAllItemQuery request, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<GetAllItemParameter>(request);
        var item = await _itemRepository.GetPagedResponseAsync(filter.PageNumber, filter.PageSize);
        var itemViewModel = _mapper.Map<IEnumerable<GetAllItemVm>>(item);
        return new PagedResponse<IEnumerable<GetAllItemVm>>(itemViewModel, filter.PageNumber, filter.PageSize); 
    }
}