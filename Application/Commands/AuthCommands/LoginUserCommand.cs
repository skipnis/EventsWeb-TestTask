using Application.Dtos;
using MediatR;

namespace Application.Commands.AuthCommands;

public class LoginUserCommand : IRequest<LoginResponse>
{
    public UserLoginDto UserLoginDto { get; set; }

    public LoginUserCommand(UserLoginDto user)
    {
        UserLoginDto = user;
    }
}