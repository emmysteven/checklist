using Checklist.Application.Common.Wrappers;
using Checklist.Application.DTOs.Entities;
using Checklist.Application.ViewModels;
using Checklist.Domain.Entities;

namespace Checklist.Application.Common.Interfaces.Services;

public interface ICheckService
{
    Task<Response<IEnumerable<CheckVm>>> GetAllAsync(string eodDate);
    Task<Check> GetByIdAsync(int id);
    Task<Response<IEnumerable<int>>> CreateAsync(CheckDto checkDto);
    Task<Check> UpdateAsync(CheckDto checkDto);
    Task<Check> DeleteAsync(string eodDate);
}