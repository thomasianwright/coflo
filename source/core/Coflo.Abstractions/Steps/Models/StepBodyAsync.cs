using Coflo.Abstractions.Execution.Models;
using Coflo.Abstractions.Steps.Contracts;

namespace Coflo.Abstractions.Steps.Models;

public abstract class StepBodyAsync : IStepBody
{
    public abstract Task<ExecutionResult> RunAsync(IStepExecutionContext context);
}