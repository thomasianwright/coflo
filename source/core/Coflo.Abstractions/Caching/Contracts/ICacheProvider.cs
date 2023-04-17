using Coflo.Abstractions.Activities.Models;
using Coflo.Abstractions.Workflows.Models;

namespace Coflo.Abstractions.Caching.Contracts;

public interface ICacheProvider
{
    Task Insert<T>(string key, T value);
    ValueTask<T?> Get<T>(string key);
    ValueTask<bool> Exists(string key);
    Task Delete(string key);
}