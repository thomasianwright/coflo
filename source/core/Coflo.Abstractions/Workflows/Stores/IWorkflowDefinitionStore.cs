using Coflo.Abstractions.Workflows.Models;

namespace Coflo.Abstractions.Workflows.Stores;

public interface IWorkflowDefinitionStore
{
    Task<WorkflowDefinition> GetWorkflowDefinitionAsync(long workflowDefinitionId);
    Task<WorkflowDefinitionVersion> GetWorkflowDefinitionVersionAsync(long workflowDefinitionId, long workflowVersionId);
    Task<IEnumerable<WorkflowDefinition>> GetWorkflowDefinitionVersionsAsync(long workflowDefinitionId);
    
    Task SaveWorkflowDefinitionAsync(WorkflowDefinition workflowDefinition);
    Task SaveWorkflowDefinitionVersionAsync(WorkflowDefinition workflowDefinition);
    
    Task DeleteWorkflowDefinitionAsync(long workflowDefinitionId);
    Task DeleteWorkflowDefinitionVersionAsync(long workflowDefinitionId, long workflowVersionId);
}