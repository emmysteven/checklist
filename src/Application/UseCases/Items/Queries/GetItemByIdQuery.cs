using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Items.Queries;

public class GetItemByIdQuery : IRequest<Item>
{
    public GetItemByIdQuery(int id)
    {
        Id = id;
    }
    public int Id { get; }
} 
    

public class GetStepByIdHandler : IRequestHandler<GetItemByIdQuery, Item>
{
    private readonly IItemRepository _itemRepository;

    public GetStepByIdHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<Item> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        var step = await _itemRepository.GetByIdAsync(request.Id);
        if (step == null) throw new ApiException($"Step Not Found.");
        return step;
    }
}