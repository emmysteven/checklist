using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Items.Commands;

public class DeleteItemCommand : IRequest<Item>
{
    public DeleteItemCommand(Item item)
    {
        Item = item;
    }

    public Item Item { get; }
}
    
public class DeleteStepHandler : IRequestHandler<DeleteItemCommand, Item>
{
    private readonly IItemRepository _itemRepository;

    public DeleteStepHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<Item> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        return await _itemRepository.DeleteAsync(request.Item);
    }
}