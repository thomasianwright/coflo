using Coflo.Abstractions.Execution.Models;
using Coflo.Abstractions.Steps.Contracts;
using Coflo.Abstractions.Workflow.Models;

namespace Coflo.Abstractions.Primitives;

public class WorkflowStepInline : WorkflowStep<InlineStepBody>
{
    public Func<IStepExecutionContext, ExecutionResult> Body { get; set; }

    public override IStepBody ConstructBody(IServiceProvider serviceProvider)
    {
        return new InlineStepBody(Body);
    }
}