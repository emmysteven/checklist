using Checklist.Application.Common.Interfaces.Services;
using Checklist.Application.DTOs.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Checklist.WebAPI.Controllers;

[Authorize]
public class TodoController : BaseController
{
    private readonly ITodoService _todoService;
    
    public TodoController(ILogger<TodoController> logger, ITodoService todoService) 
        : base(logger) {
        _todoService = todoService;
    }
        
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _todoService.GetAllAsync());
    }

    [HttpGet("{id}"), AllowAnonymous]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        return Ok(await _todoService.GetByIdAsync(id));
    }

    // POST api/<controller>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TodoDto todoDto)
    {
        return Ok(await _todoService.CreateAsync(todoDto));
    }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TodoDto todoDto)
    {
        return Ok(await _todoService.UpdateAsync(id, todoDto));
    }
    
    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _todoService.DeleteAsync(id);
        return NoContent();
    }
}