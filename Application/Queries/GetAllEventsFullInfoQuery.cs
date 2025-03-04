using Application.Dtos;
using MediatR;

namespace Application.Queries;

public class GetAllEventsFullInfoQuery : IRequest<IEnumerable<EventFullDto>>
{
    
}