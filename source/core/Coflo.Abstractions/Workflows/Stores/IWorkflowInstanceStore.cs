using Coflo.Abstractions.Workflows.Models;

namespace Coflo.Abstractions.Workflows.Stores;

public interface IWorkflowInstanceStore
{
    Task<WorkflowInstance> GetWorkflowInstanceAsync(long workflowInstanceId);
    Task<IEnumerable<WorkflowInstance>> GetWorkflowInstancesAsync(long workflowDefinitionId);
    
    Task SaveWorkflowInstanceAsync(WorkflowInstance workflowInstance, bool persist = false);
    
    Task DeleteWorkflowInstanceAsync(long workflowInstanceId);
}