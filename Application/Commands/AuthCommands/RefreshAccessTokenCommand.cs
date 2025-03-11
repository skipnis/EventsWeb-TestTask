using Application.Dtos;
using MediatR;

namespace Application.Commands.AuthCommands;

public class RefreshAccessTokenCommand : IRequest<string>
{
    public RefreshAccessTokeRequestDto Dto { get; set; }

    public RefreshAccessTokenCommand(RefreshAccessTokeRequestDto dto)
    {
        Dto = dto;
    }
}