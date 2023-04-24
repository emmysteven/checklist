using AutoMapper;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using Checklist.Domain.Common;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Items.Commands;
public record CreateItemCommand : IRequest<Response<IEnumerable<int>>>
{
    public IEnumerable<Item> Items { get; set; } = new List<Item>();
}

public class CreateItemHandler : IRequestHandler<CreateItemCommand, Response<IEnumerable<int>>>
{
    private readonly IItemRepository _itemRepo;
    private readonly IMapper _mapper;

    public CreateItemHandler(IItemRepository itemRepo, IMapper mapper)
    {
        _itemRepo = itemRepo;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<int>>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var items = _mapper.Map<IEnumerable<Item>>(request.Items).ToList();

        await _itemRepo.BulkInsertAsync(items);
    
        var itemIds = items.Select(i => i.Id);
        return new Response<IEnumerable<int>>(itemIds);
    }
}