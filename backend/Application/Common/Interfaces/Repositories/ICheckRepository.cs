using Checklist.Domain.Entities;

namespace Checklist.Application.Common.Interfaces.Repositories;

public interface ICheckRepository : IBaseRepository<Check>
{
    Task<List<Check>> GetByDate(string eodDate);
    bool CheckDate(string eodDate);
}