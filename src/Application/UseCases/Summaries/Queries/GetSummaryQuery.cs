using AutoMapper;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using MediatR;

namespace Checklist.Application.UseCases.Summaries.Queries;

public record GetSummaryQuery : IRequest<Response<IEnumerable<SummaryVm>>>
{
    public GetSummaryQuery(string eodDate)
    {
        EodDate = eodDate;
    }
    public string EodDate { get; }
}

public class GetSummaryHandler : IRequestHandler<GetSummaryQuery, Response<IEnumerable<SummaryVm>>>
{
    private readonly ISummaryRepository _repo;
    private readonly IMapper _mapper;

    public GetSummaryHandler(ISummaryRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    
    public async Task<Response<IEnumerable<SummaryVm>>> Handle(GetSummaryQuery request, CancellationToken cancellationToken)
    {
        var summary = await _repo.GetByDate(request.EodDate);
        var summaryVm = _mapper.Map<IEnumerable<SummaryVm>>(summary);
        
        return new Response<IEnumerable<SummaryVm>>(summaryVm);
    }
}