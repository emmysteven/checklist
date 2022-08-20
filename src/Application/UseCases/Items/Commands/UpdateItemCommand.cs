using AutoMapper;
using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Items.Commands;

public record UpdateItemCommand : IRequest
{
    public int Id { get; init; }
    public string? Name { get; init; }
}
    
public class UpdateTodoHandler : IRequestHandler<UpdateItemCommand>
{
    private readonly IDataContext _context;
    private readonly IMapper _mapper;

    public UpdateTodoHandler(IDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.Items
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (item == null)
        {
            throw new NotFoundException(nameof(Items), request.Id);
        }

        item = _mapper.Map<Item>(request);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
        
        
        // var isItem = await _context.Items.FindAsync(request.Id);
        // if (isItem == null) throw new ApiException("Item Not Found.");
        // var item = _mapper.Map<Item>(request);
        //
        // return await _itemRepository.UpdateAsync(item);
    }
}