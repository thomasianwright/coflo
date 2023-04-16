using Ardalis.GuardClauses;
using Coflo.Abstractions.Activities.Models;
using Coflo.Abstractions.Activities.Stores;
using Coflo.Abstractions.Caching.Contracts;

namespace Coflo.Infrastructure.Persistance.MySQL.Stores;

public class MySqlActivityDefinitionStore : IActivityDefinitionStore
{
    private readonly ICacheProvider _cacheProvider;

    public MySqlActivityDefinitionStore(ICacheProvider cacheProvider)
    {
        _cacheProvider = cacheProvider;
    }

    public Task<ActivityDefinition?> GetActivityDefinitionAsync(long activityDefinitionId)
    {
        return _cacheProvider.Get<ActivityDefinition>($"activityDefinition-{activityDefinitionId}");
    }
}