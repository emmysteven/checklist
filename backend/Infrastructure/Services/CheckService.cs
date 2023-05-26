using AutoMapper;
using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces.Repositories;
using Checklist.Application.Common.Interfaces.Services;
using Checklist.Application.Common.Wrappers;
using Checklist.Application.DTOs;
using Checklist.Application.ViewModels;
using Checklist.Domain.Entities;

namespace Checklist.Infrastructure.Services;

public class CheckService : ICheckService
{
    private readonly IMapper _mapper;
    private readonly ICheckRepository _repo;

    public CheckService(IMapper mapper, ICheckRepository repo)
    {
        _mapper = mapper;
        _repo = repo;
    }
    

    async Task<Response<IEnumerable<CheckVm>>> ICheckService.GetAllAsync(string eodDate)
    {
        var checks = await _repo.GetByDate(eodDate);
        var checkVm = _mapper.Map<IEnumerable<CheckVm>>(checks);
        
        return new Response<IEnumerable<CheckVm>>(checkVm);
    }
    
    
    async Task<Check> ICheckService.GetByIdAsync(int id)
    {
        var check = await _repo.GetByIdAsync(id);
        if (check == null) throw new ApiException("Item Not Found.");
        return check;
    }
    
    
    async Task<Response<IEnumerable<int>>> ICheckService.CreateAsync(CheckDto request)
    {
        var checks = _mapper.Map<IEnumerable<Check>>(request.Checks).ToList();
        await _repo.BulkInsertAsync(checks);
    
        var checkIds = checks.Select(i => i.Id);
        return new Response<IEnumerable<int>>(checkIds);
    }
    
    
    async Task<Check> ICheckService.UpdateAsync(CheckDto checkDto)
    {
        // var isCheck = await _repo.GetByIdAsync(checkVm.Id);
        // if (isCheck == null) throw new ApiException("Item Not Found.");

        var checks = _mapper.Map<Check>(checkDto);
        return await _repo.UpdateAsync(checks);
    }
    
    
    public async Task<Check> DeleteAsync(string eodDate)
    {
        var isCheck = await _repo.GetByDate(eodDate);
        if (isCheck == null) throw new ApiException("Item Not Found.");
        
        var check = _mapper.Map<Check>(isCheck);
        return await _repo.DeleteAsync(check);
    }
}