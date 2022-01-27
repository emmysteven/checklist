using AutoMapper;
using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Items.Commands;

public class UpdateItemCommand : IRequest<Item>
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
    
public class UpdateTodoHandler : IRequestHandler<UpdateItemCommand, Item>
{
    private readonly IMapper _mapper;
    private readonly IItemRepository _itemRepository;

    public UpdateTodoHandler(IItemRepository itemRepository, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
    }

    public async Task<Item> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var isItem = await _itemRepository.GetByIdAsync(request.Id);
        if (isItem == null) throw new ApiException("Item Not Found.");
        var item = _mapper.Map<Item>(request);

        return await _itemRepository.UpdateAsync(item);
    }
}