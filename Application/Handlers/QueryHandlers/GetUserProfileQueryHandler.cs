using Application.Dtos;
using Application.Interfaces;
using Application.Queries;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.QueryHandlers;

public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserProfileQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetById(request.Id, cancellationToken);
        
        if (user == null)
        {
            throw new Exception($"User with ID {request.Id} not found.");
        }
        
        return _mapper.Map<UserProfileDto>(user);
    }
}