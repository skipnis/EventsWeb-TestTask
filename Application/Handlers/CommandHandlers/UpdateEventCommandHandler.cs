using Application.Commands.EventCommands;
using Application.Events;
using Application.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.CommandHandlers;

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _unitOfWork.EventRepository.GetById(request.Id, cancellationToken);
        if (eventEntity == null)
        {
            throw new Exception("Event not found.");
        }
        
        _mapper.Map(request.EventUpdateDto, eventEntity);
        _unitOfWork.EventRepository.UpdateAsync(eventEntity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return eventEntity.Id;
    }
}