using Application.Dtos;
using Application.Interfaces;
using MediatR;

namespace Application.Commands.EventCommands;

public class CreateEventCommand : IRequest<Guid>
{
    public EventCreationDto EventCreationDto { get; set; }

    public CreateEventCommand(EventCreationDto eventCreationDto)
    {
        EventCreationDto = eventCreationDto;
    }
}