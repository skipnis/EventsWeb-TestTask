using Application.Dtos;
using Application.Interfaces;
using Application.Queries;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.QueryHandlers;

public class GetEventParticipantsQueryHandler : IRequestHandler<GetEventParticipantsQuery, IEnumerable<UserProfileDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEventParticipantsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserProfileDto>> Handle(GetEventParticipantsQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.EventRepository.GetParticipants(request.EventId, cancellationToken);
        return _mapper.Map<IEnumerable<UserProfileDto>>(users);
    }
}