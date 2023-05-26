using AutoMapper;
using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces.Repositories;
using Checklist.Application.Common.Interfaces.Services;
using Checklist.Application.Common.Wrappers;
using Checklist.Application.DTOs;
using Checklist.Application.ViewModels;
using Checklist.Domain.Entities;

namespace Checklist.Infrastructure.Services;

public class TodoService: ITodoService
{
    private readonly IMapper _mapper;
    private readonly ITodoRepository _repo;

    public TodoService(IMapper mapper, ITodoRepository repo)
    {
        _mapper = mapper;
        _repo = repo;
    }

    public async Task<Response<IEnumerable<TodoVm>>> GetAllAsync()
    {
        var todo = await _repo.GetAllAsync();
        var todoVm = _mapper.Map<IEnumerable<TodoVm>>(todo);
        
        return new Response<IEnumerable<TodoVm>>(todoVm); 
    }
    
    
    public async Task<Todo> GetByIdAsync(int id)
    {
        var todo = await _repo.GetByIdAsync(id);
        if (todo == null) throw new ApiException("Todo Not Found.");
        
        return todo;
    }
    
    
    public async Task<Response<int>> CreateAsync(TodoDto todoDto)
    {
        var todo = _mapper.Map<Todo>(todoDto);
        await _repo.CreateAsync(todo);
        
        return new Response<int>(todo.Id);
    }
    
    
    public async Task<Todo> UpdateAsync(int id, TodoDto todoDto)
    {
        var isTodo = await _repo.GetByIdAsync(id);
        if (isTodo == null) throw new ApiException("Todo Not Found.");
        
        var todo = _mapper.Map<Todo>(todoDto);
        return await _repo.UpdateAsync(todo);
    }
    
    
    public async Task<Todo> DeleteAsync(int id)
    {
        var todo = await _repo.GetByIdAsync(id);
        if (todo == null) throw new ApiException("Todo Not Found.");
        
        return await _repo.DeleteAsync(todo);
    }
}