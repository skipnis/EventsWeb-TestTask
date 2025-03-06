using Application.Commands.EventCommands;
using Application.Commands.UserCommands;
using Application.Dtos;
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
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var query = new GetFullEventByIdQuery(id);
        var eventFullInfo = await _mediator.Send(query);
        return Ok(eventFullInfo); 
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetFullEventsQuery();
        var events = await _mediator.Send(query);
        return Ok(events);
    }
    
    [HttpGet("paginated")]
    public async Task<IActionResult> GetAllPaginated(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 5)
    {
        var query = new GetPaginatedEventsQuery(pageNumber, pageSize);
        var events = await _mediator.Send(query);
        return Ok(events);
    }
    
    [HttpGet("preview")]
    public async Task<IActionResult> GetAllPreview()
    {
        var query = new GetEventsPreviewQuery();
        var events = await _mediator.Send(query);
        return Ok(events);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetByName([FromRoute] string name)
    {
        var query = new GetFullEventByNameQuery(name);
        var eventEnity = await _mediator.Send(query);
        return Ok(eventEnity);
    }

    [HttpGet("filtered")]
    public async Task<IActionResult> GetAllFiltered(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] List<string>? categories,
        [FromQuery] List<string>? locations)
    {
        var query = new GetFilteredEventsQuery
        (
            dateFrom,
            dateTo,
            categories,
            locations);
        
        var events = await _mediator.Send(query);
        return Ok(events);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] EventCreateDto dto)
    {
        var command = new CreateEventCommand(dto);
        var eventId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = eventId }, eventId);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent([FromRoute] Guid id, [FromBody] EventUpdateDto dto)
    {
        var command = new UpdateEventCommand(id, dto);
        var eventId = await _mediator.Send(command);
        return Ok(new { eventId }); 
    }

    [HttpPost("{id}/photo")]
    public async Task<IActionResult> AddPhoto([FromRoute] Guid id, [FromForm] AddEventImageDto dto)
    {
        var command = new AddEventImageCommand(id, dto);
        var path = await _mediator.Send(command);
        return Ok(new { path });
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserForEvent([FromBody] RegisterUserForEventCommand command)
    {
        var eventId = await _mediator.Send(command);
        return Ok(new { EventId = eventId });
    }

    [Authorize(Policy = "AdminPolicy")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent([FromRoute] Guid id)
    {
       var result = await _mediator.Send(new DeleteEventCommand(id));
       if (result)
       {
           return Ok(new { Message = "Event successfully deleted." });
       }
       return NotFound(new { Message = "Event not found." });
    }
    
    [HttpDelete("unregister")]
    public async Task<IActionResult> UnregisterUserFromEvent([FromBody] UnregisterUserFromEventCommand command)
    {
        var result = await _mediator.Send(command);
        if (result)
        {
            return Ok(new { Message = "User successfully unregistered from event" });
        }
        return NotFound(new { Message = "Registration not found or cannot be canceled" });
    }

    [Authorize(Policy = "AdminPolicy")]
    [HttpGet("{id}/participants")]
    public async Task<IActionResult> GetParticipants([FromRoute] Guid id)
    {
        var query = new GetEventParticipantsQuery(id);
        var participants = await _mediator.Send(query);
        return Ok(participants);
    }
}