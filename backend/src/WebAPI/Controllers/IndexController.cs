using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class IndexController : ControllerBase
{

    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Welcome to Checklist, Kindly use the Web Client");
    }
    
}