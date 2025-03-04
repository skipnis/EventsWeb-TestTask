using Application.Dtos;
using MediatR;

namespace Application.Queries;

public class GetFullEventsQuery : IRequest<IEnumerable<EventFullDto>>
{
    
}