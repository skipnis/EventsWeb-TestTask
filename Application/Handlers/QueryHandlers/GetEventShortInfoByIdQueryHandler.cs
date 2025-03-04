using Application.Dtos;
using Application.Interfaces;
using Application.Queries;
using Core.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.QueryHandlers;

public class GetEventShortInfoByIdQueryHandler : IRequestHandler<GetEventShortInfoByIdQuery, EventShortDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEventShortInfoByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<EventShortDto> Handle(GetEventShortInfoByIdQuery request, CancellationToken cancellationToken)
    {
        var eventEntity = await _unitOfWork.EventRepository.GetById(request.Id);
        if (eventEntity == null) throw new Exception($"Event with id {request.Id} does not exist");
        
        return _mapper.Map<EventShortDto>(eventEntity);
    }
}