using Application.Dtos;
using MediatR;

namespace Application.Commands.AuthCommands;

public class AssignRoleCommand : IRequest<bool>
{
    public RoleAssignmentRequest RoleAssignmentRequest { get; set; }

    public AssignRoleCommand(RoleAssignmentRequest roleAssignmentRequest)
    {
        RoleAssignmentRequest = roleAssignmentRequest;
    }
}