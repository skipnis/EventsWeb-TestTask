using Application.Dtos;
using Application.Interfaces;
using MediatR;

namespace Application.Commands.EventCommands;

public class CreateEventCommand : IRequest<Guid>
{
    public EventCreateDto EventCreateDto { get; set; }

    public CreateEventCommand(EventCreateDto eventCreateDto)
    {
        EventCreateDto = eventCreateDto;
    }
}