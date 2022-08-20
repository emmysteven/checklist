using AutoMapper;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using MediatR;

namespace Checklist.Application.UseCases.Items.Queries;

public class GetAllItemQuery : IRequest<PagedResponse<IEnumerable<ItemsVm>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
    
public class GetAllItemHandler : IRequestHandler<GetAllItemQuery, PagedResponse<IEnumerable<ItemsVm>>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllItemHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResponse<IEnumerable<ItemsVm>>> Handle(GetAllItemQuery request, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<GetAllItemParameter>(request);
        var item = await _context.Items.GetPagedResponseAsync(filter.PageNumber, filter.PageSize);
        var itemViewModel = _mapper.Map<IEnumerable<ItemsVm>>(item);
        return new PagedResponse<IEnumerable<ItemsVm>>(itemViewModel, filter.PageNumber, filter.PageSize); 
    }
}