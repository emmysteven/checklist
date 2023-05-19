using Microsoft.AspNetCore.Mvc;

namespace Checklist.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    private readonly ILogger<BaseController> _logger;
    
    protected BaseController(ILogger<BaseController> logger)
    {
        _logger = logger;
    }
    
    protected void Log(string message)
    {
        _logger.LogInformation("{Message}", message);
    }

    protected void LogException(Exception ex)
    {
        _logger.LogError("Exception occurred: {ExMessage}", ex.Message);
    }
}