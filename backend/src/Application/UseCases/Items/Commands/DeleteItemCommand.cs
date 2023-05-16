using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Items.Commands;
public record DeleteItemCommand(int Id) : IRequest<Item>;
public class DeleteItemHandler : IRequestHandler<DeleteItemCommand, Item>
{
    private readonly IItemRepository _repo;

    public DeleteItemHandler(IItemRepository repo)
    {
        _repo = repo;
    }

    public async Task<Item> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _repo.GetByIdAsync(request.Id);
        
        if (item == null) throw new ApiException("Item Not Found.");
        
        return await _repo.DeleteAsync(item);
    }
}