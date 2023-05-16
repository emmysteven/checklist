using Checklist.Domain.Entities;

namespace Checklist.Application.Common.Interfaces;

public interface ITodoRepository : IBaseRepository<Todo>
{
    // Task<bool> IsUniqueEmailAsync(string email);
    // Task<bool> IsUniqueWebsiteAsync(string website);
    // Task<bool> IsUniquePhoneNumberAsync(string phoneNumber);
}