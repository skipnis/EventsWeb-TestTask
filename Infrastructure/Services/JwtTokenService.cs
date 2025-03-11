using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos;
using Application.Interfaces;
using Core.Enities;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class JwtTokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IRedisTokenService _redisTokenService;

    public JwtTokenService(
        IOptions<JwtSettings> jwtSettings,
        IRedisTokenService redisTokenService
        )
    {
        _jwtSettings = jwtSettings.Value;
        _redisTokenService = redisTokenService;
    }

    public string GenerateAccessToken(Guid id, string userName, IEnumerable<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,id.ToString()),
            new Claim(ClaimTypes.Name, userName)
        };
        
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.AccessTokenExpirationInMinutes),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public Task<string> GenerateRefreshToken(Guid id)
    {
        var refreshToken =  Guid.NewGuid().ToString();
        var expirationTime = _jwtSettings.RefreshTokenExpirationInMinutes;
        _redisTokenService.StoreRefreshToken(id.ToString(), refreshToken, TimeSpan.FromDays(expirationTime));

        return Task.FromResult(refreshToken);
    }

    public async Task<string> RefreshAccessToken(RefreshAccessTokeRequestDto dto)
    {
        var storedRefreshToken = await _redisTokenService.GetRefreshTokenByUserId(dto.UserId.ToString());
        
        if (storedRefreshToken != dto.RefreshToken)
        {
            throw new UnauthorizedAccessException("Invalid refresh token.");
        }
        
        var newAccessToken = GenerateAccessToken(dto.UserId, dto.UserName, dto.Roles);
        return newAccessToken;
    }
}