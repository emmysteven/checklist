using AutoMapper;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using MediatR;

namespace Checklist.Application.UseCases.Items.Queries;

public record GetItemQuery : IRequest<Response<IEnumerable<ItemVm>>>
{
    public GetItemQuery(string eodDate)
    {
        EodDate = eodDate;
    }
    public string EodDate { get; }
}

public class GetItemHandler : IRequestHandler<GetItemQuery, Response<IEnumerable<ItemVm>>>
{
    private readonly IItemRepository _repo;
    private readonly IMapper _mapper;

    public GetItemHandler(IItemRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    
    public async Task<Response<IEnumerable<ItemVm>>> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        var item = await _repo.GetByDate(request.EodDate);
        var itemVm = _mapper.Map<IEnumerable<ItemVm>>(item);
        
        return new Response<IEnumerable<ItemVm>>(itemVm);
    }
}