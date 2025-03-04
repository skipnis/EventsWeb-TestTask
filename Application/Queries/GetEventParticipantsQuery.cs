using Application.Dtos;
using MediatR;

namespace Application.Queries;

public class GetEventParticipantsQuery : IRequest<IEnumerable<UserProfileDto>>
{
    public Guid EventId { get; set; }

    public GetEventParticipantsQuery(Guid eventId)
    {
        EventId = eventId;
    }
}