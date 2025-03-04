using MediatR;

namespace Application.Commands.UserCommands;

public class RegisterUserForEventCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }

    public RegisterUserForEventCommand(Guid userId, Guid eventId)
    {
        UserId = userId;
        EventId = eventId;
    }
}