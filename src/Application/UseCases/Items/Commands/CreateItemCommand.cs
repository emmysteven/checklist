using AutoMapper;
using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Items.Commands;
public record CreateItemCommand : IRequest<Response<int>>
{
    public int TodoId { get; set; } = default;

    public string StartTime => string.Empty;
    public string EndTime => string.Empty;
}

public class CreateItemHandler : IRequestHandler<CreateItemCommand, Response<int>>
{
    private readonly IItemRepository _itemRepo;
    private readonly ITodoRepository _todoRepo;
    private readonly IMapper _mapper;

    public CreateItemHandler(IItemRepository itemRepo, ITodoRepository todoRepo, IMapper mapper)
    {
        _itemRepo = itemRepo;
        _todoRepo = todoRepo;
        _mapper = mapper;
    }

    public async Task<Response<int>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var todo = await _todoRepo.GetByIdAsync(request.TodoId);
        if(todo == null) throw new ApiException("Todo Not Found.");

            var newItem = new Item
        {
            Todo = todo,
            StartTime = request.StartTime,
            EndTime = request.EndTime
        };
        
        var item = _mapper.Map<Item>(newItem);
        await _itemRepo.CreateAsync(item);
        
        return new Response<int>(item.Id);
    }
}