using Coflo.Abstractions.Execution.Models;
using Coflo.Abstractions.Steps.Contracts;

namespace Coflo.Abstractions.Primitives.Recurring;

public class Recur : ContainerStepBody
{
    public TimeSpan Interval { get; set; }

    public bool StopCondition { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        if (StopCondition)
        {
            return ExecutionResult.Next();
        }

        return new ExecutionResult
        {
            Proceed = false,
            BranchValues = new List<object> { null },
            SleepFor = Interval
        };
    }
}