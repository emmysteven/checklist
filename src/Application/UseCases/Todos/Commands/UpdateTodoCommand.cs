using AutoMapper;
using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Todos.Commands;

public record UpdateTodoCommand : IRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
    
public class UpdateTodoHandler : IRequestHandler<UpdateTodoCommand>
{
    private readonly IDataContext _context;
    private readonly IMapper _mapper;

    public UpdateTodoHandler(IDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _context.Todos
            .FindAsync(new object[] { request.Id }, cancellationToken);
        
        if (todo == null)
        {
            throw new NotFoundException(nameof(Items), request.Id);
        }
        
        todo = _mapper.Map<Todo>(request);
        return Unit.Value;
        // if (isTodo == null) throw new ApiException("Todo Not Found.");
        // var todo = _mapper.Map<Todo>(request);

        // return await _todoRepository.UpdateAsync(todo);
    }
}