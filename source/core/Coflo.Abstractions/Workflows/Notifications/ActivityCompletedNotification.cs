using Coflo.Abstractions.Variables.Model;
using Mediator;

namespace Coflo.Abstractions.Workflows.Notifications;

public class ActivityCompletedNotification : INotification
{
    public long NodeId { get; set; }
    public long WorkflowInstanceId { get; set; }
    public IVariableCollection Variables { get; set; }
    public string Outcome { get; set; }
}