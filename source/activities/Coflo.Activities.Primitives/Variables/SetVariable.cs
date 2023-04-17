using Coflo.Abstractions.Activities.Attributes;
using Coflo.Abstractions.Activities.Contracts;
using Coflo.Abstractions.Activities.Enums;
using Coflo.Abstractions.Activities.Models;
using Coflo.Abstractions.Variables.Model;

namespace Coflo.Activities.Primitives.Variables;

[Activity(DisplayName = "Set Variable", Category = ActivityCategory.Workflow, Outcomes = new[] { Done, Failed })]
public class SetVariable : Activity
{
    private const string Done = "Done";
    private const string Failed = "Failed";
    
    [ActivityInput(DisplayName = "Variable")]
    public VariableDefinition Variable { get; set; }

    [ActivityInput(DisplayName = "Value")]
    public object? Value { get; set; }

    public SetVariable() : base("SET_VARIABLE")
    {
    }

    public override Task<IActivityExecutionResult> ExecuteAsync(ActivityExecutionContext context)
    {
        var variable = new VariableInstance(Variable);
        variable.SetValue(Value);
        var isUpdated = context.Variables.AddOrUpdate(variable);

        var result = new ActivityExecutionResult(isUpdated ? Done : Failed, isUpdated, ActivityStatus.Completed, context.Variables,
            context.WorkflowInstanceId, context.ActivityInstanceId);

        return Task.FromResult(result as IActivityExecutionResult);
    }
}