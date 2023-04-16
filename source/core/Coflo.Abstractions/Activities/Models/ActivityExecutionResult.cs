using Coflo.Abstractions.Activities.Contracts;
using Coflo.Abstractions.Activities.Enums;
using Coflo.Abstractions.Variables.Model;

namespace Coflo.Abstractions.Activities.Models;

public class ActivityExecutionResult : IActivityExecutionResult
{
    public bool IsSuccessful { get; set; }
    public ActivityStatus Status { get; set; }
    public IVariableCollection VariableCollection { get; set; }
    public long WorkflowInstanceId { get; set; }
    public long ActivityInstanceId { get; set; }
    public string Outcome { get; set; }

    public ActivityExecutionResult(string outcome, bool isSuccessful, ActivityStatus status,
        IVariableCollection variableCollection, long workflowInstanceId, long activityInstanceId)
    {
        Outcome = outcome;
        IsSuccessful = isSuccessful;
        Status = status;
        VariableCollection = variableCollection;
    }
}