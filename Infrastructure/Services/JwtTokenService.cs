using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos;
using Application.Interfaces;
using Core.Enities;
using Core.Interfaces;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class JwtTokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IRedisTokenService _redisTokenService;
    private readonly IUnitOfWork _unitOfWork;

    public JwtTokenService(
        IOptions<JwtSettings> jwtSettings,
        IRedisTokenService redisTokenService,
        IUnitOfWork unitOfWork
        )
    {
        _jwtSettings = jwtSettings.Value;
        _redisTokenService = redisTokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> GenerateAccessToken(User user)
    {
        var roles = await _unitOfWork.UserManager.GetRolesAsync(user);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
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
        Console.WriteLine($"Generated Token: {token}");
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public Task<string> GenerateRefreshToken(User user)
    {
        var refreshToken =  Guid.NewGuid().ToString();
        var expirationTime = _jwtSettings.RefreshTokenExpirationInMinutes;
        _redisTokenService.StoreRefreshToken(user.Id.ToString(), refreshToken, TimeSpan.FromDays(expirationTime));

        return Task.FromResult(refreshToken);
    }

    public async Task<string> RefreshAccessToken(RefreshAccessTokenDto dto)
    {
        var user = await _unitOfWork.UserRepository.GetById(Guid.Parse(dto.UserId));
        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found.");
        }
        
        var storedRefreshToken = await _redisTokenService.GetRefreshTokenByUserId(dto.UserId);
        
        if (storedRefreshToken != dto.RefreshToken)
        {
            throw new UnauthorizedAccessException("Invalid refresh token.");
        }
        
        var newAccessToken = await GenerateAccessToken(user);
        return newAccessToken;
    }
}