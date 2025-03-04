using Application.Dtos;
using Application.Interfaces;
using Application.Queries;
using Core.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.QueryHandlers;

public class GetEventFullInfoByIdQueryHandler : IRequestHandler<GetEventFullInfoByIdQuery, EventFullDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEventFullInfoByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<EventFullDto> Handle(GetEventFullInfoByIdQuery request, CancellationToken cancellationToken)
    {
        var eventEntity = await _unitOfWork.EventRepository.GetById(request.Id);
        if(eventEntity == null) throw new Exception("$Event with id {request.Id} does not exist");
        return _mapper.Map<EventFullDto>(eventEntity);
    }
}