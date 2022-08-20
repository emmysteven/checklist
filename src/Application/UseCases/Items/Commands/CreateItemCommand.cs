using AutoMapper;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Items.Commands;
public class CreateItemCommand : IRequest<Response<int>>
{
    public int TodoId { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
}

public class CreateItemHandler : IRequestHandler<CreateItemCommand, Response<int>>
{
    private readonly IDataContext _context;
    private readonly IMapper _mapper;

    public CreateItemHandler(IDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<int>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Item>(request);
        _context.Items.Add(item);
        
        await _context.SaveChangesAsync(cancellationToken);
        return new Response<int>(item.Id);
    }
}