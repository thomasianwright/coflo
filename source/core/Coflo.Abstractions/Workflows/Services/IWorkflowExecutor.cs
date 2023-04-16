using Coflo.Abstractions.Variables.Model;
using Coflo.Abstractions.Workflows.Notifications;

namespace Coflo.Abstractions.Workflows.Services;

public interface IWorkflowExecutor
{
    Task<long> InitializeWorkflow(long workflowDefinitionId, VariableCollection variables);
    
    Task ExecuteWorkflow(long workflowInstanceId);
    
    Task ActivityCompleted(ActivityCompletedNotification completedNotification);
    
    Task ActivityFailed(ActivityFailedNotification failedNotification);
    
    Task WorkflowCompleted(long workflowInstanceId);
}