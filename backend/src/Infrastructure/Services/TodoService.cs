using AutoMapper;
using Checklist.Application.Common.Exceptions;
using Checklist.Application.Common.Interfaces;
using Checklist.Application.Common.Wrappers;
using Checklist.Application.DTOs;
using Checklist.Application.ViewModels;
using Checklist.Domain.Entities;
using Checklist.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Checklist.Infrastructure.Services;

public class TodoService: ITodoService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public TodoService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<Response<IEnumerable<TodoVm>>> GetAllAsync()
    {
        var todo = await _context.Todos.ToListAsync();
        var todoVm = _mapper.Map<IEnumerable<TodoVm>>(todo);
        
        return new Response<IEnumerable<TodoVm>>(todoVm); 
    }
    
    
    public async Task<Todo> GetByIdAsync(long id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) throw new ApiException("Todo Not Found.");
        
        return todo;
    }
    
    
    public async Task<Response<long>> CreateAsync(TodoDto todoDto)
    {
        var todo = _mapper.Map<Todo>(todoDto);
        await _context.Todos.AddAsync(todo);
        await _context.SaveChangesAsync();
        
        return new Response<long>(todo.Id);
    }
    
    
    public async Task<Todo> UpdateAsync(long id, TodoDto todoDto)
    {
        var isTodo = await _context.Todos.FindAsync(id);
        if (isTodo == null) throw new ApiException("Todo Not Found.");
        
        var todo = _mapper.Map<Todo>(todoDto);
        _context.Todos.Update(todo);
        await _context.SaveChangesAsync();
        
        return todo;
    }
    
    
    public async Task<Todo> DeleteAsync(long id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) throw new ApiException("Todo Not Found.");
        
        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
        return todo;
    }
}