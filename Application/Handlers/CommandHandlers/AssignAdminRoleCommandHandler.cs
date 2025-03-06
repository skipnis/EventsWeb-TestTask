using Application.Commands.UserCommands;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Handlers.CommandHandlers;

public class AssignAdminRoleCommandHandler : IRequestHandler<AssignAdminRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignAdminRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AssignAdminRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserManager.FindByIdAsync(request.UserId.ToString());
        
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        if (!await _unitOfWork.RoleManager.RoleExistsAsync("Admin"))
        {
            throw new Exception("Role not found");
        }
        
        var result = await _unitOfWork.UserManager.AddToRoleAsync(user, "Admin");   
        
        if (!result.Succeeded)
        {
            throw new Exception("Failed to assign Admin role");
        }
        return true;
    }
}