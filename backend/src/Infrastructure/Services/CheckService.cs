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

public class CheckService : ICheckService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public CheckService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    

    public async Task<Response<IEnumerable<CheckVm>>> GetAllAsync(string eodDate)
    {
        var dateValue = IpHelper.GetDate(eodDate);
        var checks = await _context.Checks.FirstOrDefaultAsync(x => x.EodDate == dateValue);
        var checkVm = _mapper.Map<IEnumerable<CheckVm>>(checks);
        
        return new Response<IEnumerable<CheckVm>>(checkVm);
    }
    
    
    public async Task<Check> GetByIdAsync(long id)
    {
        var check = await _context.Checks.FindAsync(id);
        if (check == null) throw new ApiException("Item Not Found.");
        return check;
    }
    
    
    public bool CheckDate(string eodDate)
    {
        var dateValue = IpHelper.GetDate(eodDate);
        var isCheck = _context.Checks.Any(x => x.EodDate == dateValue);
        return !isCheck;
    }
    
    
    public async Task<Response<IEnumerable<long>>> CreateAsync(CheckDto request)
    {
        var checks = _mapper.Map<IEnumerable<Check>>(request.Checks).ToList();
        await _context.BulkInsertAsync(checks);
        await _context.BulkSaveChangesAsync();
    
        var checkIds = checks.Select(i => i.Id);
        return new Response<IEnumerable<long>>(checkIds);
    }
    
    
    public async Task<Check> UpdateAsync(CheckDto checkDto)
    {
        // var isCheck = await _repo.GetByIdAsync(checkVm.Id);
        // if (isCheck == null) throw new ApiException("Item Not Found.");

        var checks = _mapper.Map<Check>(checkDto);
        _context.Checks.Update(checks);
        await _context.SaveChangesAsync();
        return checks;
    }
    
    
    public async Task<Check> DeleteAsync(string eodDate)
    {
        var dateValue = IpHelper.GetDate(eodDate);
        var isCheck = await _context.Checks.FirstOrDefaultAsync(x => x.EodDate == dateValue);
        if (isCheck == null) throw new ApiException("Check Not Found.");
        
        var check = _mapper.Map<Check>(isCheck);
        _context.Checks.Remove(check);
        await _context.SaveChangesAsync();
        return check;
    }
    
}