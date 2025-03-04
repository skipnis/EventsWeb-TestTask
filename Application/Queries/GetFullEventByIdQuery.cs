using Application.Dtos;
using MediatR;

namespace Application.Queries;

public class GetFullEventByIdQuery : IRequest<EventFullDto>
{
    public Guid Id { get; set; }

    public GetFullEventByIdQuery(Guid id)
    {
        Id = id;
    }
}