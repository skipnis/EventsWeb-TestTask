using Application.Dtos;
using MediatR;

namespace Application.Queries;

public class GetUserEventsQuery : IRequest<IEnumerable<EventPreviewDto>>
{
    public Guid UserId { get; set; }

    public GetUserEventsQuery(Guid userId)
    {
        UserId = userId;
    }
}