using Application.Commands.EventCommands;
using Application.Commands.UserCommands;
using Application.Dtos;
using Application.Events;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetFullEventByIdQuery(id);
        var eventFullInfo = await _mediator.Send(query, cancellationToken);
        return Ok(eventFullInfo); 
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetFullEventsQuery();
        var events = await _mediator.Send(query, cancellationToken);
        return Ok(events);
    }
    
    [HttpGet("paginated")]
    public async Task<IActionResult> GetAllPaginated(
        CancellationToken cancellationToken,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 5)
    {
        var query = new GetPaginatedEventsQuery(pageNumber, pageSize);
        var events = await _mediator.Send(query, cancellationToken);
        return Ok(events);
    }
    
    [HttpGet("preview")]
    public async Task<IActionResult> GetAllPreview(CancellationToken cancellationToken)
    {
        var query = new GetEventsPreviewQuery();
        var events = await _mediator.Send(query, cancellationToken);
        return Ok(events);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetByName([FromRoute] string name, CancellationToken cancellationToken)
    {
        var query = new GetFullEventByNameQuery(name);
        var eventEnity = await _mediator.Send(query, cancellationToken);
        return Ok(eventEnity);
    }

    [HttpGet("filtered")]
    public async Task<IActionResult> GetAllFiltered(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] List<string>? categories,
        [FromQuery] List<string>? locations,
        CancellationToken cancellationToken)
    {
        var query = new GetFilteredEventsQuery
        (
            dateFrom,
            dateTo,
            categories,
            locations);
        
        var events = await _mediator.Send(query, cancellationToken);
        return Ok(events);
    }
    
    [Authorize(Policy="AdminOrEventOwnerPolicy")]
    [HttpGet("{id}/participants")]
    public async Task<IActionResult> GetParticipants(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetEventParticipantsQuery(id);
        var participants = await _mediator.Send(query, cancellationToken);
        return Ok(participants);
    }
    
    [Authorize(Policy="AdminOrEventOwnerPolicy")]
    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] EventCreationDto dto, CancellationToken cancellationToken)
    {
        var command = new CreateEventCommand(dto);
        var eventId = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = eventId }, eventId);
    }

    [Authorize(Policy="AdminOrEventOwnerPolicy")]
    [HttpPost("{id}/photo")]
    public async Task<IActionResult> AddPhoto(
        [FromRoute] Guid id,
        [FromForm] AddEventImageDto dto,
        CancellationToken cancellationToken)
    {
        var command = new AddEventImageCommand(id, dto);
        var path = await _mediator.Send(command, cancellationToken);
        return Ok(new { path });
    }
    
    [Authorize]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserForEvent(
        [FromBody] RegisterUserForEventCommand command,
        CancellationToken cancellationToken)
    {
        var eventId = await _mediator.Send(command, cancellationToken);
        return Ok(new { EventId = eventId });
    }
    
        
    [Authorize(Policy="AdminOrEventOwnerPolicy")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(
        [FromRoute] Guid id,
        [FromBody] EventUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var command = new UpdateEventCommand(id, dto);
        var eventId = await _mediator.Send(command, cancellationToken);
        await _mediator.Publish(new EventUpdated(id), cancellationToken);
        return Ok(new { eventId }); 
    }

    [Authorize(Policy="AdminOrEventOwnerPolicy")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent([FromRoute] Guid id, CancellationToken cancellationToken)
    {
       var result = await _mediator.Send(new DeleteEventCommand(id), cancellationToken);
       if (result)
       {
           return Ok(new { Message = "Event successfully deleted." });
       }
       return NotFound(new { Message = "Event not found." });
    }
    
    [HttpDelete("unregister")]
    public async Task<IActionResult> UnregisterUserFromEvent(
        [FromBody] UnregisterUserFromEventCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (result)
        {
            return Ok(new { Message = "User successfully unregistered from event" });
        }
        return NotFound(new { Message = "Registration not found or cannot be canceled" });
    }
}