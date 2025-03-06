using Application.Commands.UserCommands;
using Application.Dtos;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userRegistrationDto)
    {
        var command = new RegisterUserCommand(userRegistrationDto);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var command = new LoginUserCommand(dto);
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] Guid userId)
    {
        var command = new LogoutCommand(userId);
        var result = await _mediator.Send(command);
        if (result)
        {
            return Ok(new { message = "Logged out successfully." });
        }
        
        return BadRequest(new { message = "Failed to logout" });
    }
    
    [Authorize]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshAccessToken([FromBody] RefreshAccessTokenDto dto)
    {
        var command = new RefreshAccessTokenCommand(dto);
        var response = await _mediator.Send(command);
        return Ok(response);
    }
    
    [Authorize(Policy = "AdminPolicy")]
    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] RoleAssignmentRequest request)
    {
        var command = new AssignRoleCommand(request);
        var result = await _mediator.Send(command);
        if (result)
        {
            return Ok(new { message = "Role assigned successfully." });
        }
        return BadRequest(new { message = "Failed to assign role" });
    }
}