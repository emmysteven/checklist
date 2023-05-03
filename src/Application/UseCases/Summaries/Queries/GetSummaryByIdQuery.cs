using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Summaries.Queries;

public class GetSummaryByIdQuery : IRequest<Summary>
{
    public GetSummaryByIdQuery(int id)
    {
        Id = id;
    }
    public int Id { get; }
} 

public class GetSummaryByIdHandler : IRequestHandler<GetSummaryByIdQuery, Summary>
{
    private readonly ISummaryRepository _repo;
    
    public GetSummaryByIdHandler(ISummaryRepository repo)
    {
        _repo = repo;
    }

    public async Task<Summary> Handle(GetSummaryByIdQuery request, CancellationToken cancellationToken)
    {
        var summary = await _repo.GetByIdAsync(request.Id);
        if (summary == null) throw new ApiException("Summary Not Found.");
        return summary;
    }
}