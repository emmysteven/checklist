using Checklist.Domain.Entities;

namespace Checklist.Application.Common.Interfaces.Repositories;

public interface ISummaryRepository : IBaseRepository<Summary>
{
    Task<List<Summary>> GetByDate(string eodDate);
}
