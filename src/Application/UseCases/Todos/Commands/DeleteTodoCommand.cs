using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Application.UseCases.Todos.Commands;
public record DeleteTodoCommand(int Id) : IRequest;
public class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand>
{
    private readonly IDataContext _context;

    public DeleteTodoHandler(IDataContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _context.Todos
            .Where(x => x.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (todo == null)
        {
            throw new NotFoundException(nameof(Todo), request.Id);
        }
        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}