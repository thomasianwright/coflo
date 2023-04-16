using Coflo.Abstractions.Execution.Models;
using Coflo.Abstractions.Steps.Contracts;
using Coflo.Abstractions.Steps.Models;

namespace Coflo.Abstractions.Primitives.Control;

public class Decide : StepBody
{
    public object Expression { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        return ExecutionResult.Outcome(Expression);
    }
}