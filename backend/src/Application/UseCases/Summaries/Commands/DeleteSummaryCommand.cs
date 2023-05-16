using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.UseCases.Items.Commands;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Summaries.Commands;

public record DeleteSummaryCommand(int Id) : IRequest<Summary>;

public class DeleteSummaryHandler : IRequestHandler<DeleteSummaryCommand, Summary>
{
    private readonly ISummaryRepository _repo;

    public DeleteSummaryHandler(ISummaryRepository repo)
    {
        _repo = repo;
    }

    public async Task<Summary> Handle(DeleteSummaryCommand request, CancellationToken cancellationToken)
    {
        var summary = await _repo.GetByIdAsync(request.Id);
        
        if (summary == null) throw new ApiException("Summary Not Found.");
        
        return await _repo.DeleteAsync(summary);
    }
}