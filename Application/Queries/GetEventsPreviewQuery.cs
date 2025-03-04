using Application.Dtos;
using MediatR;

namespace Application.Queries;

public class GetEventsPreviewQuery : IRequest<IEnumerable<EventPreviewDto>>
{
    
}