using Application.Commands.EventCommands;
using Application.Interfaces;
using Core.Enities;
using Core.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.CommandHandlers;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = _mapper.Map<Event>(request.EventCreateDto);
        
        await _unitOfWork.EventRepository.AddAsync(eventEntity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return eventEntity.Id;
    }
}