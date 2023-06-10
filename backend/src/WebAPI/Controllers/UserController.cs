using Checklist.Application.Common.Interfaces;
using Checklist.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await _userService.GetUsersAsync());
    }

    // POST api/<controller>
    [HttpPost("add")]
    public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
    {
        return Ok(await _userService.AddUserAsync(userDto));
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] AuthDto authDto)
    {
        return Ok(_userService.AuthenticateAsync(authDto));
    }
}