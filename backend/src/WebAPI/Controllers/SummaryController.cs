using Checklist.Application.Common.Interfaces;
using Checklist.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class SummaryController : BaseController
{
    private readonly ISummaryService _summaryService;

    public SummaryController(ILogger<SummaryController> logger, ISummaryService summaryService) 
        : base(logger)
    {
        _summaryService = summaryService;
    }
        
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string eodDate)
    {
        var query = await _summaryService.GetAllAsync(eodDate);
        return Ok(query);
    }

    [HttpGet("{id}"), AllowAnonymous]
    public async Task<IActionResult> Get([FromRoute] long id)
    {
        var query = await _summaryService.GetByIdAsync(id);
        return Ok(query);
    }

    // POST api/<controller>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SummaryDto summaryDto)
    {
        return Ok(await _summaryService.CreateAsync(summaryDto));
    }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] long id, [FromBody] SummaryDto summaryDto)
    {
        return Ok(await _summaryService.UpdateAsync(id,summaryDto));
    }
    
    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _summaryService.DeleteAsync(id);
        return NoContent();
    }
}