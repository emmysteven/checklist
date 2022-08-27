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

public class GetItemByIdHandler : IRequestHandler<GetItemByIdQuery, Item>
{
    private readonly IItemRepository _repo;
    public GetItemByIdHandler(IItemRepository repo)
    {
        _repo = repo;
    }

    public async Task<Item> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repo.GetByIdAsync(request.Id);
        if (item == null) throw new ApiException("Item Not Found.");
        return item;
    }
}