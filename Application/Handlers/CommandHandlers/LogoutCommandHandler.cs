using Application.Commands.AuthCommands;
using Application.Interfaces;
using MediatR;

namespace Application.Handlers.CommandHandlers;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
{
    private readonly IRedisTokenService _redisTokenService;

    public LogoutCommandHandler(IRedisTokenService redisTokenService)
    {
        _redisTokenService = redisTokenService;
    }

    public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await _redisTokenService.RemoveRefreshToken(request.UserId.ToString());
        return true; 
    }
}