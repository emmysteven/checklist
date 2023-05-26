using Checklist.Application.Common.Interfaces.Services;
using Checklist.Application.DTOs.Account;
using Microsoft.AspNetCore.Mvc;

namespace Checklist.WebAPI.Controllers;

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
    public async Task<IActionResult> Get()
    {
        return Ok(await _userService.GetAllAsync());
    }

    // POST api/<controller>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var origin = Request.Headers["origin"];
        return Ok(await _userService.RegisterAsync(request, origin));
    }

    // [HttpPost("login")]
    // public async Task<IActionResult> Login([FromBody] AuthRequest request)
    // {
    //     var response = await _userService.AuthenticateAsync(request, GenerateIpAddress());
    //     SetTokenCookie(response.RefreshToken);
    //     return Ok(response);
    // }
    
    [HttpPost("login")]
    public IActionResult Login([FromBody] AuthRequest request)
    {
        var response = _userService.AuthenticateAsync(request, GenerateIpAddress());
        SetTokenCookie(response.RefreshToken);
        return Ok(response);
    }

    [HttpGet("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromQuery] int id, string token)
    {
        return Ok(await _userService.VerifyEmailAsync(id, token));
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        await _userService.ForgotPasswordAsync(request, Request.Headers["origin"]);
        return Ok(new {message = "Please check your email for password reset instructions"});
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        return Ok(await _userService.ResetPasswordAsync(request));
    }

    // [HttpPut("{id}")]
    // public async Task<IActionResult> Update(int id, [FromBody] UpdateShopCommand command)
    // {
    //     if (id != command.Id) return BadRequest();
    //     return Ok(await Mediator.Send(command));
    // }

    // DELETE api/<controller>/5
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> Delete(int id)
    // {
    //     var query = await Mediator.Send(new GetUserByIdQuery(id));
    //     return Ok(await Mediator.Send(new DeleteUserCommand(query)));
    // }
        
    private void SetTokenCookie(string? token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddHours(7)
        };
        Response.Cookies.Append("refreshToken", token!, cookieOptions);
    }

    private string GenerateIpAddress()
    {
        if (Request.Headers.TryGetValue("X-Forwarded-For", out var header))
            return header!;
        return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()!;
    }
}