using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Application.UseCases.Items.Commands;
public record DeleteItemCommand(int Id) : IRequest;
public class DeleteItemHandler : IRequestHandler<DeleteItemCommand>
{
    private readonly IDataContext _context;

    public DeleteItemHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.Items
            .Where(x => x.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (item == null)
        {
            throw new NotFoundException(nameof(Item), request.Id);
        }
        
        _context.Items.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}