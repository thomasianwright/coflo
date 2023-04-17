using Coflo.Abstractions.Activities.Models;
using Coflo.Abstractions.Workflows.Models;

namespace Coflo.Abstractions.Activities.Stores;

public interface IActivityDefinitionStore
{
    ValueTask<ActivityDefinition?> GetActivityDefinitionAsync(long activityDefinitionId);
    ValueTask<WorkflowDefinitionVersion?> GetWorkflowDefinitionVersionAsync(long workflowDefinitionVersionId);
}