using Checklist.Application.Common.Wrappers;
using Checklist.Application.DTOs;
using Checklist.Application.ViewModels;
using Checklist.Domain.Entities;

namespace Checklist.Application.Common.Interfaces;

public interface ISummaryService
{
    Task<Response<IEnumerable<SummaryVm>>> GetAllAsync(string eodDate);
    Task<Summary> GetByIdAsync(long id);
    Task<Response<long>> CreateAsync(SummaryDto summaryDto);
    Task<Summary> UpdateAsync(long id, SummaryDto summaryDto);
    Task<Response<SummaryVm>> CheckedAsync(CheckedDto checkedDto);
    Task<Summary> DeleteAsync(long id);
}