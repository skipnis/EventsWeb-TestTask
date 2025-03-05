using MediatR;

namespace Application.Commands.UserCommands;

public class LogoutCommand : IRequest<bool>
{
    public Guid UserId { get; }

    public LogoutCommand(Guid userId)
    {
        UserId = userId;
    }
}