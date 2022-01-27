using AutoMapper;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Items.Commands;
public class CreateItemCommand : IRequest<Response<int>>
{
    public string? Name { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
}
    
public class CreateItemHandler : IRequestHandler<CreateItemCommand, Response<int>>
{
    private readonly IMapper _mapper;
    private readonly IItemRepository _itemRepository;

    public CreateItemHandler(IMapper mapper, IItemRepository itemRepository)
    {
        _mapper = mapper;
        _itemRepository = itemRepository;
    }

    public async Task<Response<int>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Item>(request);
        await _itemRepository.CreateAsync(item);
        return new Response<int>(item.Id);
    }
}