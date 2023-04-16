using Coflo.Abstractions.Execution.Models;
using Coflo.Abstractions.Steps.Contracts;
using Coflo.Abstractions.Steps.Models;

namespace Coflo.Abstractions.Primitives.Control;

public class Delay : StepBody
{
    public TimeSpan Period { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        if (context.PersistenceData != null)
        {
            return ExecutionResult.Next();
        }
            
        return ExecutionResult.Sleep(Period, true);
    }
}