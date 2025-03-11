using Application.Dtos;
using Application.Interfaces;
using Application.Queries;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.QueryHandlers;

public class GetFilteredEventsQueryHandler : IRequestHandler<GetFilteredEventsQuery, IEnumerable<EventFullDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetFilteredEventsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EventFullDto>> Handle(GetFilteredEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await _unitOfWork.EventRepository.GetFilteredEvents(
            request.DateFrom,
            request.DateTo,
            request.Categories, 
            request.Locations,
            cancellationToken);
        
        return _mapper.Map<IEnumerable<EventFullDto>>(events);
    }
}