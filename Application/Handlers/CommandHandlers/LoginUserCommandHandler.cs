using Application.Commands.AuthCommands;
using Application.Dtos;
using Application.Interfaces;
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
        
        var userRoles = await _unitOfWork.UserManager.GetRolesAsync(user);
        
        var accessToken = _tokenService.GenerateAccessToken(user.Id, request.UserLoginDto.Username, userRoles);
        var refreshToken = await _tokenService.GenerateRefreshToken(user.Id);

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}