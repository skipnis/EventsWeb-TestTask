using Application.Commands.AuthCommands;
using Application.Interfaces;
using MediatR;

namespace Application.Handlers.CommandHandlers;

public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserManager.FindByIdAsync(request.RoleAssignmentRequest.UserId.ToString());
        
        if (user == null) throw new Exception($"User with id {request.RoleAssignmentRequest.UserId} does not exist");
        
        var role = await _unitOfWork.RoleManager.FindByNameAsync(request.RoleAssignmentRequest.Role);
        
        if(role == null) throw new Exception($"Role with id {request.RoleAssignmentRequest.Role} does not exist");
        
        var result = await _unitOfWork.UserManager.AddToRoleAsync(user, request.RoleAssignmentRequest.Role);
        
        if(!result.Succeeded) throw new Exception($"Failed to add role {request.RoleAssignmentRequest.Role}");
        
        return true;
    }
}