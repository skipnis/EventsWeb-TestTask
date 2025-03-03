using Application.Dtos;
using MediatR;

namespace Application.Queries;

public class GetEventShortInfoQuery : IRequest<EventShortDto>, IRequest<Guid>
{
    public Guid Id { get; set; }

    public GetEventShortInfoQuery(Guid id)
    {
        Id = id;
    }
}