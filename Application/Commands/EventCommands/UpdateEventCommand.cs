using Application.Dtos;
using MediatR;

namespace Application.Commands.EventCommands;

public class UpdateEventCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public EventUpdateDto EventUpdateDto { get; set; }

    public UpdateEventCommand(Guid id,EventUpdateDto eventUpdateDto)
    {
        Id = id;
        EventUpdateDto = eventUpdateDto;
    }
}