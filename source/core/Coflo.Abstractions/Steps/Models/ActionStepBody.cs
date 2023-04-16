using Coflo.Abstractions.Execution.Models;
using Coflo.Abstractions.Steps.Contracts;

namespace Coflo.Abstractions.Steps.Models;

public class ActionStepBody : StepBody
{
    public Action<IStepExecutionContext> Body { get; set; }
        
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Body(context);
        
        return ExecutionResult.Next();
    }
}