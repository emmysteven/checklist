using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checklist.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    private readonly ILogger<BaseController> _logger;
    private ISender? _mediator;
    protected BaseController(ILogger<BaseController> logger)
    {
        _logger = logger;
    }
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

}