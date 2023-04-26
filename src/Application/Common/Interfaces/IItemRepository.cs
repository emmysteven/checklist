using Checklist.Domain.Entities;

namespace Checklist.Application.Common.Interfaces;

public interface IItemRepository : IBaseRepository<Item>
{
    Task<List<Item>> GetByDate(string eodDate);
}