using Application.Dtos;
using MediatR;

namespace Application.Commands.UserCommands;

public class RefreshAccessTokenCommand : IRequest<string>
{
    public RefreshAccessTokenDto Dto { get; set; }

    public RefreshAccessTokenCommand(RefreshAccessTokenDto dto)
    {
        Dto = dto;
    }
}