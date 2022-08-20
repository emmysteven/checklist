using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Items.Queries;

public class GetItemByIdQuery : IRequest<Item>
{
    public GetItemByIdQuery(int id)
    {
        Id = id;
    }
    public int Id { get; }
} 
    

public class GetStepByIdHandler : IRequestHandler<GetItemByIdQuery, Item>
{
    private readonly IDataContext _context;

    public GetStepByIdHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Item> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.Items.FindAsync(request.Id);
        if (item == null) throw new ApiException("Item Not Found.");
        return item;
    }
}