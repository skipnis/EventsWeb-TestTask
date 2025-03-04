using Application.Dtos;
using Application.Interfaces;
using Application.Queries;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.QueryHandlers;

public class GetAllEventsShortInfoQueryHandler : IRequestHandler<GetAllEventsShortInfoQuery, IEnumerable<EventShortDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllEventsShortInfoQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<EventShortDto>> Handle(GetAllEventsShortInfoQuery request, CancellationToken cancellationToken)
    {
        var events = await _unitOfWork.EventRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EventShortDto>>(events);
    }
}