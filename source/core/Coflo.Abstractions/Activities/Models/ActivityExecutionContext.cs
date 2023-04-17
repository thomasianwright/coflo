using Coflo.Abstractions.Variables.Model;

namespace Coflo.Abstractions.Activities.Models;

public class ActivityExecutionContext
{
    public ActivityExecutionContext(IVariableCollection variables, long workflowInstanceId, long activityInstanceId, string activityName)
    {
        Variables = variables;
        WorkflowInstanceId = workflowInstanceId;
        ActivityInstanceId = activityInstanceId;
        ActivityName = activityName;
    }

    public IVariableCollection Variables { get; set; }
    public long WorkflowInstanceId { get; set; }
    public long ActivityInstanceId { get; set; }
    public string ActivityName { get; set; }
}