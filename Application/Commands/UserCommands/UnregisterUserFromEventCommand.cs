using MediatR;

namespace Application.Commands.UserCommands;

public class UnregisterUserFromEventCommand : IRequest<bool>
{
    public Guid EventId { get; set; }
    public Guid UserId { get; set; }

    public UnregisterUserFromEventCommand(Guid eventId, Guid userId)
    {
        EventId = eventId;
        UserId = userId;
    }
}