using Application.Dtos;
using Application.Interfaces;
using Application.Queries;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.QueryHandlers;

public class GetEventShortInfoByNameQueryHandler : IRequestHandler<GetEventShortInfoByNameQuery, EventShortDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEventShortInfoByNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<EventShortDto> Handle(GetEventShortInfoByNameQuery request, CancellationToken cancellationToken)
    {
        var eventEntity = await _unitOfWork.EventRepository.GetByName(request.Name);
        if(eventEntity == null) throw new Exception($"Event with name {request.Name} not found");
        return _mapper.Map<EventShortDto>(eventEntity);
    }
}
