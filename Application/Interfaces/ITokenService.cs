using Application.Dtos;
using Core.Enities;

namespace Application.Interfaces;

public interface ITokenService
{
    Task<string> GenerateAccessToken(User user);
    Task<string> GenerateRefreshToken(User user);
    Task<string> RefreshAccessToken(RefreshAccessTokenDto dto);
}