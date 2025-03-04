using Application.Dtos;
using MediatR;

namespace Application.Queries;

public class GetEventShortInfoByIdQuery : IRequest<EventShortDto>
{
    public Guid Id { get; set; }

    public GetEventShortInfoByIdQuery(Guid id)
    {
        Id = id;
    }
}