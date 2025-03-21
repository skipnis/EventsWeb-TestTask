using Application.Commands.AuthCommands;
using Application.Dtos;
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
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userRegistrationDto, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(userRegistrationDto);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto, CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(dto);
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] Guid userId, CancellationToken cancellationToken)
    {
        var command = new LogoutCommand(userId);
        var result = await _mediator.Send(command, cancellationToken);
        if (result)
        {
            return Ok(new { message = "Logged out successfully." });
        }
        
        return BadRequest(new { message = "Failed to logout" });
    }
    
    [Authorize]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshAccessToken([FromBody] RefreshAccessTokeRequestDto dto, CancellationToken cancellationToken)
    {
        var command = new RefreshAccessTokenCommand(dto);
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }
    
    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] RoleAssignmentRequest request, CancellationToken cancellationToken)
    {
        var command = new AssignRoleCommand(request);
        var result = await _mediator.Send(command, cancellationToken);
        if (result)
        {
            return Ok(new { message = "Role assigned successfully." });
        }
        return BadRequest(new { message = "Failed to assign role" });
    }
}