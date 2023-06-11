using Checklist.Application.Common.Interfaces;
using Checklist.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
public class CheckController : BaseController
{
    private readonly ICheckService _checkService;

    public CheckController(ILogger<CheckController> logger, ICheckService checkService) 
        : base(logger)
    {
        _checkService = checkService;
    }
        
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string eodDate)
    {
        var query = await _checkService.GetAllAsync(eodDate);
        return Ok(query);
    }
    
    // [HttpGet]
    // public IActionResult CheckDate([FromQuery] string eodDate)
    // {
    //     var query = _checkService.CheckDate(eodDate);
    //     return Ok(query);
    // }

    [HttpGet("{id}"), AllowAnonymous]
    public async Task<IActionResult> Get([FromRoute] long id)
    {
        var query = await _checkService.GetByIdAsync(id);
        return Ok(query);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CheckDto checkDto )
    {
        return Ok(await _checkService.CreateAsync(checkDto));
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CheckDto checkDto)
    {
        return Ok(await _checkService.UpdateAsync(checkDto));
    }
    
    [HttpDelete("{eodDate}")]
    public async Task<IActionResult> Delete([FromBody] string eodDate)
    {
        await _checkService.DeleteAsync(eodDate);
        return NoContent();
    }
}