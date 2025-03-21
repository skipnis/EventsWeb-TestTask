using Application.Dtos;
using MediatR;

namespace Application.Commands.AuthCommands;

public class RegisterUserCommand : IRequest<Guid>
{
    public UserRegistrationDto UserRegistrationDto { get; set; }

    public RegisterUserCommand(UserRegistrationDto userRegistrationDto)
    {
        UserRegistrationDto = userRegistrationDto;
    }
}