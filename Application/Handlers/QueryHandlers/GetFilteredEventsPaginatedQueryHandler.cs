using Application.Dtos;
using Application.Interfaces;
using Application.Queries;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.QueryHandlers;

public class GetFilteredEventsPaginatedQueryHandler : IRequestHandler<GetFilteredEventsPaginatedQuery, List<EventFullDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public async Task<List<EventFullDto>> Handle(GetFilteredEventsPaginatedQuery request, CancellationToken cancellationToken)
    {
        var eventsQuery = await _unitOfWork.EventRepository.GetAllAsync();
        
        if (request.Filter.DateFrom.HasValue)
        {
            eventsQuery = eventsQuery.Where(e => e.Date >= request.Filter.DateFrom.Value);
        }

        if (request.Filter.DateTo.HasValue)
        {
            eventsQuery = eventsQuery.Where(e => e.Date <= request.Filter.DateTo.Value);
        }

        if (request.Filter.Categories != null && request.Filter.Categories.Any())
        {
            eventsQuery = eventsQuery.Where(e => request.Filter.Categories.Contains(e.Category.Name));
        }

        if (request.Filter.Locations != null && request.Filter.Locations.Any())
        {
            eventsQuery = eventsQuery.Where(e => request.Filter.Locations.Contains(e.Place.Address));
        }
        
        var events = eventsQuery
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize);
        
        return _mapper.Map<List<EventFullDto>>(events);
        
    }
}