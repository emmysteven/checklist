using Checklist.Application.Common.Wrappers;
using Checklist.Application.DTOs;
using Checklist.Application.ViewModels;
using Checklist.Domain.Entities;

namespace Checklist.Application.Common.Interfaces;

public interface ICheckService
{
    Task<Response<IEnumerable<CheckVm>>> GetAllAsync(string eodDate);
    Task<Check> GetByIdAsync(long id);
    Task<Response<IEnumerable<long>>> CreateAsync(CheckDto checkDto);
    Task<Check> UpdateAsync(CheckDto checkDto);
    Task<Check> DeleteAsync(string eodDate);
}