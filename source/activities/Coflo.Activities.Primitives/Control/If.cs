using Coflo.Abstractions.Activities.Attributes;
using Coflo.Abstractions.Activities.Contracts;
using Coflo.Abstractions.Activities.Enums;
using Coflo.Abstractions.Activities.Models;
using Coflo.Abstractions.Evaluation.Contracts;
using Coflo.Abstractions.Evaluation.Models;
using Coflo.Abstractions.Variables.Model;

namespace Coflo.Activities.Primitives.Control;

[Activity(DisplayName = "If", Outcomes = new[] { "True", "False" }, Category = ActivityCategory.Decision)]
public class If : Activity
{
    private readonly IEvaluator _evaluator;

    [ActivityInput(DisplayName = "Condition")]
    public string Condition { get; set; } = "false";

    public If(IEvaluator evaluator) : base("IF")
    {
        _evaluator = evaluator;
    }

    public override async Task<IActivityExecutionResult> ExecuteAsync(ActivityExecutionContext context)
    {
        try
        {
            var evaluationContext = new EvaluationContext(context.Variables, Condition);
            var result = await _evaluator.EvaluateAsync(evaluationContext);

            return new ActivityExecutionResult(result ? "True" : "False", true, ActivityStatus.Completed,
                new VariableCollection(), context.WorkflowInstanceId, context.ActivityInstanceId);
        }
        catch (Exception e)
        {
            return new ActivityExecutionResult(string.Empty, false, ActivityStatus.Faulted,
                new VariableCollection(), context.WorkflowInstanceId, context.ActivityInstanceId);
        }
    }
}