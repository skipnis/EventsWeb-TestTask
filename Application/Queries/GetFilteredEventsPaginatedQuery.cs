using Application.Dtos;
using MediatR;

namespace Application.Queries;

public class GetFilteredEventsPaginatedQuery : IRequest<List<EventFullDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public EventFilter? Filter { get; set; }

    public GetFilteredEventsPaginatedQuery(int pageNumber, int pageSize, EventFilter? filter)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Filter = filter;
    }
}