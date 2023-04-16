using Coflo.Abstractions.Variables.Model;
using Mediator;

namespace Coflo.Abstractions.Activities.Commands;

public class StartActivityCommand : ICommand
{
    public long WorkflowInstanceId { get; set; }
    public long ActivityDefinitionId { get; set; }
    public IVariableCollection VariableCollection { get; set; }
    public StartActivityCommand(long workflowInstanceId, long activityDefinitionId, IVariableCollection variableCollection)
    {
        WorkflowInstanceId = workflowInstanceId;
        ActivityDefinitionId = activityDefinitionId;
        VariableCollection = variableCollection;
    }
}