using Coflo.Abstractions.Caching.Contracts;
using Coflo.Abstractions.Workflows.Models;
using Coflo.Abstractions.Workflows.Stores;

namespace Coflo.Infrastructure.Persistance.MySQL.Stores;

public class MySqlWorkflowDefinitionStore : IWorkflowDefinitionStore
{
    private readonly ICacheProvider _cacheProvider;

    public MySqlWorkflowDefinitionStore(ICacheProvider cacheProvider)
    {
        _cacheProvider = cacheProvider;
    }

    public Task<WorkflowDefinition> GetWorkflowDefinitionAsync(long workflowDefinitionId)
    {
        return _cacheProvider.Get<WorkflowDefinition>($"workflowDefinition-{workflowDefinitionId}");
    }

    public Task<WorkflowDefinitionVersion> GetWorkflowDefinitionVersionAsync(long workflowDefinitionId, long workflowVersionId)
    {
        return _cacheProvider.Get<WorkflowDefinitionVersion>($"workflowDefinitionVersion-{workflowDefinitionId}-{workflowVersionId}");
    }

    public Task<IEnumerable<WorkflowDefinition>> GetWorkflowDefinitionVersionsAsync(long workflowDefinitionId)
    {
        throw new NotImplementedException();
    }

    public Task SaveWorkflowDefinitionAsync(WorkflowDefinition workflowDefinition)
    {
        throw new NotImplementedException();
    }

    public Task SaveWorkflowDefinitionVersionAsync(WorkflowDefinition workflowDefinition)
    {
        throw new NotImplementedException();
    }

    public Task DeleteWorkflowDefinitionAsync(long workflowDefinitionId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteWorkflowDefinitionVersionAsync(long workflowDefinitionId, long workflowVersionId)
    {
        throw new NotImplementedException();
    }
}