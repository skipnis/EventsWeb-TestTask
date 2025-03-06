using Application.Commands.AuthCommands;
using Application.Interfaces;
using Core.Enities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

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
        
        var result = await _unitOfWork.UserManager.CreateAsync(
            user,
            request.UserRegistrationDto.Password);
        
        if (!result.Succeeded)
        {
            var errorMessages = result.Errors.Select(e => e.Description).ToList();
            throw new ApplicationException("User registration failed. " + string.Join(", ", errorMessages));
        }
        
        if (!await _unitOfWork.RoleManager.RoleExistsAsync("User"))
        {
            await _unitOfWork.RoleManager.CreateAsync(new IdentityRole<Guid>("User"));
        }
        
        await _unitOfWork.UserManager.AddToRoleAsync(user, "User");
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return user.Id;
    }
}