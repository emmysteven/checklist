using Checklist.Application.Common.Wrappers;
using Checklist.Application.DTOs.Entities;
using Checklist.Application.ViewModels;
using Checklist.Domain.Entities;

namespace Checklist.Application.Common.Interfaces.Services;

public interface ISummaryService
{
    Task<Response<IEnumerable<SummaryVm>>> GetAllAsync(string eodDate);
    Task<Summary> GetByIdAsync(int id);
    Task<Response<int>> CreateAsync(SummaryDto summaryDto);
    Task<Summary> UpdateAsync(int id, SummaryDto summaryDto);
    Task<Summary> DeleteAsync(int id);
}