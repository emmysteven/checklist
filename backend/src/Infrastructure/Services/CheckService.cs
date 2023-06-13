using AutoMapper;
using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using Checklist.Application.DTOs;
using Checklist.Application.ViewModels;
using Checklist.Domain.Entities;
using Checklist.Infrastructure.Helpers;
using Checklist.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Infrastructure.Services;

public class CheckService : ICheckService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly DbSet<Check> _check;
    private readonly ICurrentUserService _currentUser;

    public CheckService(IMapper mapper, DataContext context, ICurrentUserService currentUser)
    {
        _mapper = mapper;
        _context = context;
        _currentUser = currentUser;
        _check = context.Set<Check>();
    }
    

    public async Task<Response<IEnumerable<CheckVm>>> GetAllAsync(string eodDate)
    {
        var dateValue = IpHelper.GetDate(eodDate);
        var checks = await _check.Where(x => x.EodDate == dateValue).ToListAsync();
        var checkVm = _mapper.Map<IEnumerable<CheckVm>>(checks);
        
        return new Response<IEnumerable<CheckVm>>(checkVm);
    }
    
    
    public async Task<Check> GetByIdAsync(long id)
    {
        var check = await _check.FindAsync(id);
        if (check == null) throw new ApiException("Item Not Found.");
        return check;
    }
    
    
    public bool CheckDate(string eodDate)
    {
        var dateValue = IpHelper.GetDate(eodDate);
        var isCheck = _check.Any(x => x.EodDate == dateValue);
        return !isCheck;
    }
    
    
    public async Task<Response<IEnumerable<long>>> CreateAsync(CheckDto request)
    {
        var checks = _mapper.Map<IEnumerable<Check>>(request.Checks).ToList();
        
        foreach (var check in checks)
        {
            check.MakerId = _currentUser.Username;
        }
        
        await _check.BulkInsertAsync(checks);
        
        var checkIds = checks.Select(i => i.Id);
        return new Response<IEnumerable<long>>(checkIds);
    }
    
    
    public async Task<Check> UpdateAsync(CheckDto checkDto)
    {
        foreach (var check in checkDto.Checks)
        {
            var isCheck = await _check.FindAsync(check.EodDate);
            if (isCheck == null) throw new ApiException("Item Not Found.");

            var updatedCheck = _mapper.Map<Check>(check);
            _check.Update(updatedCheck);
        }
        return checkDto.Checks.FirstOrDefault()!;
    }
    
    public async Task<Response<IEnumerable<CheckVm>>> CheckedAsync(CheckedDto checkedDto)
    {
        var dateValue = IpHelper.GetDate(checkedDto.EodDate);
        var isCheck = _check.Any(x => x.EodDate == dateValue);
        if (isCheck)
        {
            const string sql = @"
            UPDATE [corebanking].[dbo].[Chk_Check]
            SET CheckerId = @CheckerId,
                CheckerDt = @CheckerDt,
                AuthStatus = @AuthStatus
            WHERE EodDate = @EodDate";

            object[] parameters = {
                new SqlParameter("@CheckerId", _currentUser.Username!),
                new SqlParameter("@CheckerDt", DateTime.UtcNow),
                new SqlParameter("@AuthStatus", true),
                new SqlParameter("@EodDate", dateValue)
            };

            await _context.Database.ExecuteSqlRawAsync(sql, parameters);

            var checks = await _check.Where(x => x.EodDate == dateValue).ToListAsync();
            var checkVm = _mapper.Map<IEnumerable<CheckVm>>(checks);
            return new Response<IEnumerable<CheckVm>>(checkVm);
        }
        return new Response<IEnumerable<CheckVm>>(Enumerable.Empty<CheckVm>(), "No checks found");
    }
    
    
    public async Task<Check> DeleteAsync(string eodDate)
    {
        var dateValue = IpHelper.GetDate(eodDate);
        var isCheck = await _check.FirstOrDefaultAsync(x => x.EodDate == dateValue);
        if (isCheck == null) throw new ApiException("Check Not Found.");
        
        var check = _mapper.Map<Check>(isCheck);
        _check.Remove(check);
        return check;
    }
    
}