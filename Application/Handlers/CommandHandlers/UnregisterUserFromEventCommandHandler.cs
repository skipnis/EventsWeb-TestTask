using Application.Commands.UserCommands;
using Application.Interfaces;
using MediatR;

namespace Application.Handlers.CommandHandlers;

public class UnregisterUserFromEventCommandHandler : IRequestHandler<UnregisterUserFromEventCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UnregisterUserFromEventCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UnregisterUserFromEventCommand request, CancellationToken cancellationToken)
    {
        var registration = await _unitOfWork.EventUserRepository
            .GetByEventAndUserAsync(request.EventId, request.UserId);
        
        if(registration == null) throw new Exception("There is no such registration");
        
        await _unitOfWork.EventUserRepository.RemoveAsync(request.EventId, request.UserId);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}