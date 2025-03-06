using Application.Dtos;
using MediatR;

namespace Application.Commands.EventCommands;

public class AddEventImageCommand : IRequest<string>
{
    public Guid EventId { get; set; }
    public AddEventImageDto ImageDto { get; set; }

    public AddEventImageCommand(Guid eventId, AddEventImageDto imageDto)
    {
        EventId = eventId;
        ImageDto = imageDto;
    }
}