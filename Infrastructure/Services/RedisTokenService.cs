using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Infrastructure.Services;

public class RedisTokenService : IRedisTokenService
{
    private readonly IConnectionMultiplexer _redis;

    public RedisTokenService(IConnectionMultiplexer redisConnection)
    {
        _redis = redisConnection;
    }

    public async Task StoreRefreshToken(string userId, string refreshToken, TimeSpan expiration)
    {
        var db = _redis.GetDatabase();
        await db.StringSetAsync(userId, refreshToken, expiration);
    }

    public async Task<string> GetRefreshTokenByUserId(string userId)
    {
        var db = _redis.GetDatabase();
        return await db.StringGetAsync(userId);
    }

    public async Task<bool> RemoveRefreshToken(string userId)
    {
        var db = _redis.GetDatabase();
        return await db.KeyDeleteAsync(userId);
    }
}