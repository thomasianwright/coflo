using Coflo.Abstractions.Workflows.Models;

namespace Coflo.Abstractions.Caching.Contracts;

public interface ICacheProvider
{
    Task Insert<T>(string key, T value);
    Task<WorkflowDefinition> Get<T>(string key);
    Task<bool> Exists(string key);
    Task Delete(string key);
}