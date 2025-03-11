using Application.Commands.EventCommands;
using Application.Interfaces;
using MediatR;

namespace Application.Handlers.CommandHandlers;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEventCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _unitOfWork.EventRepository.GetById(request.Id);
        if (eventEntity == null)
        {
            throw new Exception($"Event with id {request.Id} does not exist");
        }
        _unitOfWork.EventRepository.DeleteAsync(eventEntity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}