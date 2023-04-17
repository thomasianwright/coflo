using Coflo.Abstractions.Workflows.Models;

namespace Coflo.Infrastructure.Persistance.Cassandra.Repositories;

public interface IWorkflowDefinitionRepository
{
    ValueTask<WorkflowDefinition?> GetWorkflowDefinition(long workflowDefinitionId);
    ValueTask<WorkflowDefinitionVersion?> GetWorkflowDefinitionVersion(long workflowDefinitionId,
        long workflowVersionId);
    Task InsertWorkflowDefinition(WorkflowDefinition workflowDefinition);
    Task InsertWorkflowDefinitionVersion(WorkflowDefinitionVersion workflowDefinitionVersion);
    Task RemoveWorkflowDefinition(long workflowDefinitionId);
}