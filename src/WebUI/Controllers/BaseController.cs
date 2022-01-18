using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checklist.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    private readonly ILogger<BaseController> _logger;
    private IMediator _mediator;

    protected BaseController(ILogger<BaseController> logger)
    {
        _logger = logger;
    }

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}