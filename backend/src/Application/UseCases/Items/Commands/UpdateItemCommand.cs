using AutoMapper;
using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Items.Commands;

public record UpdateItemCommand : IRequest<Item>
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
    
public class UpdateTodoHandler : IRequestHandler<UpdateItemCommand, Item>
{
    private readonly IItemRepository _repo;
    private readonly IMapper _mapper;

    public UpdateTodoHandler(IItemRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Item> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var isItem = await _repo.GetByIdAsync(request.Id);
        if (isItem == null) throw new ApiException("Item Not Found.");

        var item = _mapper.Map<Item>(request);

        return await _repo.UpdateAsync(item);
    }
}