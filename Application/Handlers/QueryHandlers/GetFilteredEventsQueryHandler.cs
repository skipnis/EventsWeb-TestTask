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
        var eventsQuery = await _unitOfWork.EventRepository.GetAllAsQueryableAsync();
        
        if (request.DateFrom.HasValue)
        {
            eventsQuery = eventsQuery.Where(e => e.Date >= request.DateFrom.Value);
        }

        if (request.DateTo.HasValue)
        {
            eventsQuery = eventsQuery.Where(e => e.Date <= request.DateTo.Value);
        }

        if (request.Categories != null && request.Categories.Any())
        {
            eventsQuery = eventsQuery.Where(e => request.Categories.Contains(e.Category.Name));
        }

        if (request.Locations != null && request.Locations.Any())
        {
            eventsQuery = eventsQuery.Where(e => request.Locations.Contains(e.Place.Address));
        }
        
        var events = await eventsQuery.ToListAsync();
        
        return _mapper.Map<IEnumerable<EventFullDto>>(events);
    }
}