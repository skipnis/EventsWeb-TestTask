using Application.Commands.AuthCommands;
using Application.Interfaces;
using MediatR;

namespace Application.Handlers.CommandHandlers;

public class RefreshAccessTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, string>
{
    private readonly ITokenService _tokenService;

    public RefreshAccessTokenCommandHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<string> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var newAccessToken = await _tokenService.RefreshAccessToken(request.Dto);
        return newAccessToken;
    }
}