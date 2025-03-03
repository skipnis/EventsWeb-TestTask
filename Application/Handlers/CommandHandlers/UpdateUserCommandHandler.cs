using Application.Commands.UserCommands;
using Application.Interfaces;
using Core.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.CommandHandlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetById(request.Id);
        if (user == null)
        {
            throw new Exception("User not found.");
        }
        
        _mapper.Map(request.UserUpdateDto, user);
        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return user.Id;
    }
}