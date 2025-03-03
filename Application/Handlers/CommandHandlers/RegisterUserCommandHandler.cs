using Application.Commands.UserCommands;
using Application.Interfaces;
using Core.Enities;
using Core.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.CommandHandlers;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.UserRegistrationDto);
        
        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}