using Application.Dtos;
using Application.Interfaces;
using Application.Queries;
using Core.Enities;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.QueryHandlers;

public class GetPaginatedEventsQueryHandler : IRequestHandler<GetPaginatedEventsQuery, List<EventFullDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPaginatedEventsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<EventFullDto>> Handle(GetPaginatedEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await _unitOfWork.EventRepository.GetPaginatedAsync(request.PageNumber, request.PageSize);
        return _mapper.Map<List<EventFullDto>>(events);
    }
}