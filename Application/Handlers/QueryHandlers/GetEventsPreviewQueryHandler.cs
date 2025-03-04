using Application.Dtos;
using Application.Interfaces;
using Application.Queries;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.QueryHandlers;

public class GetEventsPreviewQueryHandler : IRequestHandler<GetEventsPreviewQuery, IEnumerable<EventPreviewDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEventsPreviewQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<EventPreviewDto>> Handle(GetEventsPreviewQuery request, CancellationToken cancellationToken)
    {
        var events = await _unitOfWork.EventRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EventPreviewDto>>(events);
    }
}