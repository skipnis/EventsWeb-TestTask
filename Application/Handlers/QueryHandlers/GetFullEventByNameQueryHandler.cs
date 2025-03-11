using Application.Dtos;
using Application.Interfaces;
using Application.Queries;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.QueryHandlers;

public class GetFullEventByNameQueryHandler : IRequestHandler<GetFullEventByNameQuery, EventFullDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetFullEventByNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<EventFullDto> Handle(GetFullEventByNameQuery request, CancellationToken cancellationToken)
    {
        var eventEntity = await _unitOfWork.EventRepository.GetByName(request.Name, cancellationToken);
        if(eventEntity == null) throw new Exception($"Event with name {request.Name} not found");
        return _mapper.Map<EventFullDto>(eventEntity);
    }
}