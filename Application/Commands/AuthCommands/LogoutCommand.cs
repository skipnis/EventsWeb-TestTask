using MediatR;

namespace Application.Commands.AuthCommands;

public class LogoutCommand : IRequest<bool>
{
    public Guid UserId { get; }

    public LogoutCommand(Guid userId)
    {
        UserId = userId;
    }
}