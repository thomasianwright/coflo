using Coflo.Abstractions.Activities.Enums;
using Coflo.Abstractions.Variables.Model;

namespace Coflo.Abstractions.Activities.Contracts;

public interface IActivityInstance
{
    public long WorkflowInstanceId { get; set; }
    public long ActivityInstanceId { get; set; }
    public string ActivityName { get; set; }
    public ActivityInstanceStatus Status { get; set; }
    
    public VariableCollection VariableCollection { get; set; }
}