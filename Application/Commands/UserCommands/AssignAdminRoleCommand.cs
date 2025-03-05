using MediatR;

namespace Application.Commands.UserCommands;

public class AssignAdminRoleCommand : IRequest
{
    public Guid UserId { get; set; }

    public AssignAdminRoleCommand(Guid userId)
    {
        UserId = userId;
    }
}