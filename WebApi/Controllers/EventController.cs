using Application.Commands.EventCommands;
using Application.Commands.UserCommands;
using Application.Dtos;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("short/{id}")]
    public async Task<IActionResult> GetShortById(Guid id)
    {
        var query = new GetEventShortInfoByIdQuery(id);
        var eventShortInfo = await _mediator.Send(query);
        return Ok(eventShortInfo); 
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] EventCreateDto dto)
    {
        var command = new CreateEventCommand(dto);
        var eventId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetShortById), new { id = eventId }, eventId);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserForEvent([FromBody] RegisterUserForEventCommand command)
    {
        var eventId = await _mediator.Send(command);
        return Ok(new { EventId = eventId });
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventUpdateDto dto)
    {
        var command = new UpdateEventCommand(id, dto);
        var eventId = await _mediator.Send(command);
        return Ok(new { eventId }); 
    }
    
    [HttpDelete("unregister")]
    public async Task<IActionResult> UnregisterUserFromEvent([FromBody] UnregisterUserFromEventCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound(new { Message = "Registration not found or cannot be canceled" });
        }
        return Ok(new { Message = "User successfully unregistered from event" });
    }
}