using AutoMapper;
using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using Checklist.Application.DTOs;
using Checklist.Application.ViewModels;
using Checklist.Domain.Entities;
using Checklist.Infrastructure.Helpers;
using Checklist.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Infrastructure.Services;

public class SummaryService : ISummaryService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly ICurrentUserService _currentUser;

    public SummaryService(IMapper mapper, DataContext context, ICurrentUserService currentUser)
    {
        _mapper = mapper;
        _context = context;
        _currentUser = currentUser;
    }
    

    public async Task<Response<IEnumerable<SummaryVm>>> GetByDateAsync(string eodDate)
    {
        var dateValue = IpHelper.GetDate(eodDate);
        var summary = await _context.Summary.Where(x => x.EodDate == dateValue).ToListAsync();
        var summaryVm = _mapper.Map<IEnumerable<SummaryVm>>(summary);
        
        return new Response<IEnumerable<SummaryVm>>(summaryVm);
    }
    
    
    public async Task<Summary> GetByIdAsync(long id)
    {
        var summary = await _context.Summary.FindAsync(id);
        if (summary == null) throw new ApiException("Summary Not Found.");
        return summary;
    }
    
    
    public async Task<Response<long>> CreateAsync(SummaryDto summaryDto)
    {
        var summary = _mapper.Map<Summary>(summaryDto);
        await _context.Summary.AddAsync(summary);
        await _context.SaveChangesAsync();
        
        return new Response<long>(summary.Id);
    }


    public async Task<Summary> UpdateAsync(long id, SummaryDto summaryDto)
    {
        var isSummary = await _context.Summary.FindAsync(id);
        if (isSummary == null) throw new ApiException("Item Not Found.");

        var summary = _mapper.Map<Summary>(summaryDto);
         _context.Summary.Update(summary);
         await _context.SaveChangesAsync();
         return summary;
    }
    
    public async Task<Response<SummaryVm>> CheckedAsync(CheckedDto checkedDto)
    {
        var dateValue = IpHelper.GetDate(checkedDto.EodDate);
        var summary = await _context.Summary.FirstOrDefaultAsync(x => x.EodDate == dateValue);

        if (summary == null) return new Response<SummaryVm>("No Summary found");

        summary.CheckerId = _currentUser.Username!;
        summary.AuthStatus = true;
        var summaryVm = _mapper.Map<SummaryVm>(summary);
        _context.Summary.Update(summary);
        await _context.SaveChangesAsync();
        return new Response<SummaryVm>(summaryVm);
    }
    
    
    public async Task<Summary> DeleteAsync(long id)
    {
        var summary = await _context.Summary.FindAsync(id);
        if (summary == null) throw new ApiException("Summary Not Found.");
        
        _context.Summary.Remove(summary);
        await _context.SaveChangesAsync();
        return summary;
    }
}