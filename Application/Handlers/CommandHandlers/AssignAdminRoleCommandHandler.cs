using Application.Commands.UserCommands;
using Application.Interfaces;
using MediatR;

namespace Application.Handlers.CommandHandlers;

public class AssignAdminRoleCommandHandler : IRequestHandler<AssignAdminRoleCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignAdminRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AssignAdminRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserManager.FindByIdAsync(request.UserId.ToString());
        
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        var result = await _unitOfWork.UserManager.AddToRoleAsync(user, "Admin");
        
        if (!result.Succeeded)
        {
            throw new Exception("Failed to assign Admin role");
        }
    }
}