using MediatR;

namespace Application.Commands.UserCommands;

public class AssignAdminRoleCommand : IRequest<bool>
{
    public Guid UserId { get; set; }

    public AssignAdminRoleCommand(Guid userId)
    {
        UserId = userId;
    }
}