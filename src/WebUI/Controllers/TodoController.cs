using Checklist.Application.UseCases.Todos.Commands;
using Checklist.Application.UseCases.Todos.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Checklist.WebUI.Controllers;

[Authorize]
public class TodoController : BaseController
{
    public TodoController(ILogger<TodoController> logger) : base(logger) { }
        
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = await Mediator.Send(new GetTodoQuery());
        return Ok(query);
    }

    [HttpGet("{id}"), AllowAnonymous]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var query = await Mediator.Send(new GetTodoByIdQuery(id));
        return Ok(query);
    }

    // POST api/<controller>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTodoCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTodoCommand command)
    {
        if (id != command.Id) return BadRequest();
        return Ok(await Mediator.Send(command));
    }
    
    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTodoCommand(id));
        return NoContent();
    }
}