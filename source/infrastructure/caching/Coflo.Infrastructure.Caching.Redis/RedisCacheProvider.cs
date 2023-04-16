using System.Text.Json;
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

    public Task<WorkflowDefinition> Get<T>(string key)
    {
        var redisValue = _database.StringGet(new RedisKey(key));
        return redisValue.IsNullOrEmpty
            ? default
            : Task.FromResult(JsonSerializer.Deserialize<T>(redisValue, _jsonSerializerOptions));
    }

    public Task<bool> Exists(string key)
    {
        return _database.KeyExistsAsync(new RedisKey(key));
    }

    public Task Delete(string key)
    {
        return _database.KeyDeleteAsync(new RedisKey(key));
    }
}