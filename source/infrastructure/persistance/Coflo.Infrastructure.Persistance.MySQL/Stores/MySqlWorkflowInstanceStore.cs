using Coflo.Abstractions.Workflows.Models;
using Coflo.Abstractions.Workflows.Stores;

namespace Coflo.Infrastructure.Persistance.MySQL.Stores;

public class MySqlWorkflowInstanceStore : IWorkflowInstanceStore
{
    public Task<WorkflowInstance> GetWorkflowInstanceAsync(long workflowInstanceId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<WorkflowInstance>> GetWorkflowInstancesAsync(long workflowDefinitionId)
    {
        throw new NotImplementedException();
    }

    public Task SaveWorkflowInstanceAsync(WorkflowInstance workflowInstance, bool persist = false)
    {
        throw new NotImplementedException();
    }

    public Task DeleteWorkflowInstanceAsync(long workflowInstanceId)
    {
        throw new NotImplementedException();
    }
}