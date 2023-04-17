namespace Coflo.Abstractions.Workflows.Notifications;

public class ActivityFailedNotification
{
    public long NodeId { get; set; }
    public long WorkflowInstanceId { get; set; }
    public string Error { get; set; }
}