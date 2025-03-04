using Application.Dtos;
using Application.Interfaces;
using Application.Queries;
using Core.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.QueryHandlers;

public class GetAllEventsFullInfoQueryHandler : IRequestHandler<GetAllEventsFullInfoQuery, IEnumerable<EventFullDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllEventsFullInfoQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EventFullDto>> Handle(GetAllEventsFullInfoQuery request, CancellationToken cancellationToken)
    {
        var events = await _unitOfWork.EventRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EventFullDto>>(events);
    }
}