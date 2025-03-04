using Application.Dtos;
using MediatR;

namespace Application.Queries;

public class GetEventShortInfoByNameQuery : IRequest<EventShortDto>
{
    public string Name {get; set;}

    public GetEventShortInfoByNameQuery(string name)
    {
        Name = name;
    }
}