using Application.Dtos;
using MediatR;

namespace Application.Queries;

public class GetPaginatedEventsQuery : IRequest<List<EventFullDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; } 

    public GetPaginatedEventsQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}