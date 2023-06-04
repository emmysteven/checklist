using Checklist.Application.Common.Wrappers;
using Checklist.Application.DTOs;
using Checklist.Application.ViewModels;
using Checklist.Domain.Entities;

namespace Checklist.Application.Common.Interfaces.Services;

public interface ITodoService
{
    Task<Response<IEnumerable<TodoVm>>> GetAllAsync();
    Task<Todo> GetByIdAsync(long id);
    Task<Response<long>> CreateAsync(TodoDto todoDto);
    Task<Todo> UpdateAsync(long id, TodoDto todoDto);
    Task<Todo> DeleteAsync(long id);
}