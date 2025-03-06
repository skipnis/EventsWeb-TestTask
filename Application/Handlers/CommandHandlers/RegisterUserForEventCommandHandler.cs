using System.Numerics;
using Application.Commands.UserCommands;
using Application.Interfaces;
using Core.Enities;
using Core.Interfaces;
using MediatR;

namespace Application.Handlers.CommandHandlers;

public class RegisterUserForEventCommandHandler : IRequestHandler<RegisterUserForEventCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserForEventCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Guid> Handle(RegisterUserForEventCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetById(request.UserId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var eventEntity = await _unitOfWork.EventRepository.GetById(request.EventId);
        if (eventEntity == null)
        {
            throw new Exception("Event not found");
        }
        
        var existingRegistration = await _unitOfWork.EventUserRepository
            .GetByEventAndUserAsync(request.EventId, user.Id);

        if (existingRegistration != null)
        {
            throw new Exception("User is already registered for this event");
        }

        var participants = await _unitOfWork.EventRepository.GetParticipants(request.EventId);
        if (participants != null)
        {
            var count =  participants.ToList().Count;
            if (count == eventEntity.MaximumParticipants)
            {
                throw new Exception("Cannot register for event, because of the limit of participants");
            }
        }
        
        var eventUser = new EventUser
        {
            UserId = request.UserId,
            EventId = request.EventId,
            RegisteredAt = DateTime.UtcNow
        };
        
        await _unitOfWork.EventUserRepository.AddAsync(eventUser);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return eventEntity.Id;
    }
}