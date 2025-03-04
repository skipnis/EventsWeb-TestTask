using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("profile/{id}")]
    public async Task<IActionResult> GetProfile(Guid id)
    {
        var query = new GetUserProfileQuery(id);
        var userProfileDto = await _mediator.Send(query);
        return Ok(userProfileDto);
    }
    
}