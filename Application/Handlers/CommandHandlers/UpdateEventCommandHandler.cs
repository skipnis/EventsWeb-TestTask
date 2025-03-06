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
    private readonly IMediator _mediator;

    public UpdateEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _unitOfWork.EventRepository.GetById(request.Id);
        if (eventEntity == null)
        {
            throw new Exception("Event not found.");
        }
        
        _mapper.Map(request.EventUpdateDto, eventEntity);
        await _unitOfWork.EventRepository.UpdateAsync(eventEntity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        await _mediator.Publish(new EventUpdated(request.Id));
        
        return eventEntity.Id;
    }
}