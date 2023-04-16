using Coflo.Abstractions.Activities.Models;

namespace Coflo.Abstractions.Activities.Stores;

public interface IActivityDefinitionStore
{
    Task<ActivityDefinition> GetActivityDefinitionAsync(long activityDefinitionId);
}