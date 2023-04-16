using Coflo.Abstractions.Variables.Model;

namespace Coflo.Abstractions.Activities.Contracts;

public interface IActivityExecutionContext
{
    public VariableCollection Variables { get; set; }
    public long WorkflowInstanceId { get; set; }
    public long ActivityInstanceId { get; set; }
    public string ActivityName { get; set; }
}