using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("profile/{id}")]
    public async Task<IActionResult> GetProfile([FromRoute] Guid id)
    {
        var query = new GetUserProfileQuery(id);
        var userProfileDto = await _mediator.Send(query);
        return Ok(userProfileDto);
    }

    [Authorize]
    [HttpGet("{id}/events")]
    public async Task<IActionResult> GetEvents([FromRoute] Guid id)
    {
        var query = new GetUserEventsQuery(id);
        var events = await _mediator.Send(query);
        return Ok(events);
    }
}