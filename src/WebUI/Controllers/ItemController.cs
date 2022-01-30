using Checklist.Application.UseCases.Items.Commands;
using Checklist.Application.UseCases.Items.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Checklist.WebUI.Controllers;

[Authorize]
public class ItemController : BaseController
{
    public ItemController(ILogger<ItemController> logger) : base(logger) { }
        
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAllItemParameter filter)
    {
        var query = await Mediator.Send(new GetAllItemQuery
        {
            PageSize = filter.PageSize,
            PageNumber = filter.PageNumber
        });
        var checks = query.Data!.Select(x => new GetAllItemVm
        {
            Id = x.Id,
            Name = x.Name,
        });
        query.Data = checks;
        return Ok(query);
    }

    [HttpGet("{id}"), AllowAnonymous]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var query = await Mediator.Send(new GetItemByIdQuery(id));
        return Ok(query);
    }

    // POST api/<controller>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateItemCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateItemCommand command)
    {
        if (id != command.Id) return BadRequest();
        return Ok(await Mediator.Send(command));
    }
    
    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = await Mediator.Send(new GetItemByIdQuery(id));
        return Ok(await Mediator.Send(new DeleteItemCommand(query)));
    }
}