using AutoMapper;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using Checklist.Domain.Entities;
using MediatR;

namespace Checklist.Application.UseCases.Summaries.Commands;

public record CreateSummaryCommand : IRequest<Response<int>>
{
    public string Makers { get; set; } = string.Empty;
    public string Checkers { get; set; } = string.Empty;
    public string Dbas { get; set; } = string.Empty;
    public DateTime? EodDate { get; set; }
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public int? TxnCount { get; set; } 
}

public class CreateSummaryHandler : IRequestHandler<CreateSummaryCommand, Response<int>>
{
    private readonly ISummaryRepository _repo;
    private readonly IMapper _mapper;

    public CreateSummaryHandler(ISummaryRepository summaryRepo, IMapper mapper)
    {
        _repo = summaryRepo;
        _mapper = mapper;
    }

    public async Task<Response<int>> Handle(CreateSummaryCommand request, CancellationToken cancellationToken)
    {
        var summary = _mapper.Map<Summary>(request);
        await _repo.CreateAsync(summary);
        
        return new Response<int>(summary.Id);
    }
}