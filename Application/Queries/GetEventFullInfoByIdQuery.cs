using Application.Dtos;
using MediatR;

namespace Application.Queries;

public class GetEventFullInfoByIdQuery : IRequest<EventFullDto>
{
    public Guid Id { get; set; }

    public GetEventFullInfoByIdQuery(Guid id)
    {
        Id = id;
    }
}