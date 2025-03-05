using Application.Commands.UserCommands;
using Application.Dtos;
using Application.Interfaces;
using Core.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.CommandHandlers;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public LoginUserCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserManager.FindByNameAsync(request.UserLoginDto.Username);
        if (user == null || !await _unitOfWork.UserManager.CheckPasswordAsync(user, request.UserLoginDto.Password))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }
        
        var accessToken = await _tokenService.GenerateAccessToken(user);
        var refreshToken = await _tokenService.GenerateRefreshToken(user);

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}