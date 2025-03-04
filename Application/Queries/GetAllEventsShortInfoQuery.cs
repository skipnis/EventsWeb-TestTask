using Application.Dtos;
using MediatR;

namespace Application.Queries;

public class GetAllEventsShortInfoQuery : IRequest<IEnumerable<EventShortDto>>
{
    
}