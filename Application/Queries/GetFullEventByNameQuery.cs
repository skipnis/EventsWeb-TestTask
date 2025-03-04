using Application.Dtos;
using MediatR;

namespace Application.Queries;

public class GetFullEventByNameQuery : IRequest<EventFullDto>
{
    public string Name {get; set;}

    public GetFullEventByNameQuery(string name)
    {
        Name = name;
    }
}