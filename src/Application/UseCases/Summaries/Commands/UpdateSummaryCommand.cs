using AutoMapper;
using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Summaries.Commands;

public record UpdateSummaryCommand : IRequest<Summary>
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
    
public class UpdateSummaryHandler : IRequestHandler<UpdateSummaryCommand, Summary>
{
    private readonly ISummaryRepository _repo;
    private readonly IMapper _mapper;

    public UpdateSummaryHandler(ISummaryRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Summary> Handle(UpdateSummaryCommand request, CancellationToken cancellationToken)
    {
        var isSummary = await _repo.GetByIdAsync(request.Id);
        if (isSummary == null) throw new ApiException("Item Not Found.");

        var summary = _mapper.Map<Summary>(request);

        return await _repo.UpdateAsync(summary);
    }
}