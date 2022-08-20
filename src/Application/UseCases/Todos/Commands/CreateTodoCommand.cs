using AutoMapper;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Todos.Commands;
public class CreateTodoCommand : IRequest<Response<int>>
{
    public string Name { get; set; } = String.Empty;
}
    
public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, Response<int>>
{
    private readonly IDataContext _context;
    private readonly IMapper _mapper;

    public CreateTodoHandler(IDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<int>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = _mapper.Map<Todo>(request);
        _context.Todos.Add(todo);
        
        await _context.SaveChangesAsync(cancellationToken);
        return new Response<int>(todo.Id);
    }
}