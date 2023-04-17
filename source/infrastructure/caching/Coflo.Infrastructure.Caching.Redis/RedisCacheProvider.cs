using System.Text.Json;
using Coflo.Abstractions.Activities.Models;
using Coflo.Abstractions.Caching.Contracts;
using Coflo.Abstractions.Workflows.Models;
using StackExchange.Redis;

namespace Coflo.Infrastructure.Caching.Redis;

public class RedisCacheProvider : ICacheProvider
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly IDatabase _database;

    private static JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public RedisCacheProvider(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
        _database = _connectionMultiplexer.GetDatabase();
    }

    public Task Insert<T>(string key, T value)
    {
        return _database.SetAddAsync(new RedisKey(key),
            new RedisValue(JsonSerializer.Serialize(value, _jsonSerializerOptions)));
    }

    public async ValueTask<T?> Get<T>(string key)
    {
        var redisValue = await _database.StringGetAsync(new RedisKey(key));
        return redisValue.IsNullOrEmpty
            ? default
            : JsonSerializer.Deserialize<T>(redisValue, _jsonSerializerOptions);
    }

    public async ValueTask<bool> Exists(string key)
    {
        return await _database.KeyExistsAsync(new RedisKey(key));
    }

    public Task Delete(string key)
    {
        return _database.KeyDeleteAsync(new RedisKey(key));
    }
}