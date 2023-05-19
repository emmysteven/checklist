using AutoMapper;
using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces.Repositories;
using Checklist.Application.Common.Interfaces.Services;
using Checklist.Application.Common.Wrappers;
using Checklist.Application.DTOs.Entities;
using Checklist.Application.ViewModels;
using Checklist.Domain.Entities;

namespace Checklist.Infrastructure.Services;

public class SummaryService : ISummaryService
{
    private readonly IMapper _mapper;
    private readonly ISummaryRepository _repo;

    public SummaryService(IMapper mapper, ISummaryRepository repo)
    {
        _mapper = mapper;
        _repo = repo;
    }
    

    public async Task<Response<IEnumerable<SummaryVm>>> GetAllAsync(string eodDate)
    {
        var summary = await _repo.GetByDate(eodDate);
        var summaryVm = _mapper.Map<IEnumerable<SummaryVm>>(summary);
        
        return new Response<IEnumerable<SummaryVm>>(summaryVm);
    }
    
    
    public async Task<Summary> GetByIdAsync(int id)
    {
        var summary = await _repo.GetByIdAsync(id);
        if (summary == null) throw new ApiException("Summary Not Found.");
        return summary;
    }
    
    
    public async Task<Response<int>> CreateAsync(SummaryDto summaryDto)
    {
        var summary = _mapper.Map<Summary>(summaryDto);
        await _repo.CreateAsync(summary);
        
        return new Response<int>(summary.Id);
    }
    
    
    public async Task<Summary> UpdateAsync(int id, SummaryDto summaryDto)
    {
        var isSummary = await _repo.GetByIdAsync(id);
        if (isSummary == null) throw new ApiException("Item Not Found.");

        var summary = _mapper.Map<Summary>(summaryDto);
        return await _repo.UpdateAsync(summary);
    }
    
    
    public async Task<Summary> DeleteAsync(int id)
    {
        var summary = await _repo.GetByIdAsync(id);
        if (summary == null) throw new ApiException("Summary Not Found.");
        
        return await _repo.DeleteAsync(summary);
    }
}