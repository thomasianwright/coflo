using Coflo.Abstractions.Activities.Enums;
using Coflo.Abstractions.Variables.Model;

namespace Coflo.Abstractions.Activities.Contracts;

public interface IActivityExecutionResult
{
    public bool IsSuccessful { get; set; }
    public ActivityStatus Status { get; set; }
    public IVariableCollection VariableCollection { get; set; }
    public long WorkflowInstanceId { get; set; }
    public long ActivityInstanceId { get; set; }
    public string Outcome { get; set; }
}