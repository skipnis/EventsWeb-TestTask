using Application.Dtos;
using MediatR;

namespace Application.Queries;

public class GetEventFullInfoByNameQuery : IRequest<EventFullDto>
{
    public string Name {get; set;}

    public GetEventFullInfoByNameQuery(string name)
    {
        Name = name;
    }
}