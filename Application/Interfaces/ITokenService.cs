using Application.Dtos;
using Core.Enities;

namespace Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(Guid id, string userName, IEnumerable<string> roles);
    Task<string> GenerateRefreshToken(Guid id);
    Task<string> RefreshAccessToken(RefreshAccessTokeRequestDto dto);
}