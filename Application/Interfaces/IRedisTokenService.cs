namespace Application.Interfaces;

public interface IRedisTokenService 
{
    Task StoreRefreshToken(string userId, string refreshToken, TimeSpan expiration);
    
    Task<string> GetRefreshTokenByUserId(string userId);
    
    Task<bool> RemoveRefreshToken(string userId);
}