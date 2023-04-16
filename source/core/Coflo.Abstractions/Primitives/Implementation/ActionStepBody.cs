using Coflo.Abstractions.Execution.Models;
using Coflo.Abstractions.Steps.Contracts;
using Coflo.Abstractions.Steps.Models;

namespace Coflo.Abstractions.Primitives;

public class ActionStepBody : StepBody
{
    public Action<IStepExecutionContext> Body { get; set; }
        
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Body(context);
        return ExecutionResult.Next();
    }
}