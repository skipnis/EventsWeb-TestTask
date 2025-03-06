using Application.Dtos;
using Application.Interfaces;
using Application.Queries;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.QueryHandlers;

public class GetUserEventsQueryHandler : IRequestHandler<GetUserEventsQuery, IEnumerable<EventPreviewDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserEventsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EventPreviewDto>> Handle(GetUserEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await _unitOfWork.UserRepository.GetUserEvents(request.UserId);
        return _mapper.Map<IEnumerable<EventPreviewDto>>(events);
    }
}