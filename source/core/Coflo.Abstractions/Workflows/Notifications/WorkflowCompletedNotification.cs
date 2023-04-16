using Mediator;

namespace Coflo.Abstractions.Workflows.Notifications;

public class WorkflowCompletedNotification : INotification
{
    public long WorkflowInstanceId { get; }

    public WorkflowCompletedNotification(long workflowInstanceId)
    {
        WorkflowInstanceId = workflowInstanceId;
    }
}