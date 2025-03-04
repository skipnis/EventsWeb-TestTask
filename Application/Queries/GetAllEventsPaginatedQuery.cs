using Application.Dtos;
using MediatR;

namespace Application.Queries;

public class GetAllEventsPaginatedQuery : IRequest<List<EventFullDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;

    public GetAllEventsPaginatedQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}