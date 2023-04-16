using Coflo.Abstractions.Execution.Models;
using Coflo.Abstractions.Steps.Contracts;

namespace Coflo.Abstractions.Steps.Models;

public abstract class StepBody : IStepBody
{
    public abstract ExecutionResult Run(IStepExecutionContext context);

    public Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        return Task.FromResult(Run(context));
    }

    protected Execution.Models.ExecutionResult OutcomeResult(object value)
    {
        return new Execution.Models.ExecutionResult
        {
            Proceed = true,
            OutcomeValue = value
        };
    }

    protected ExecutionResult PersistResult(object persistenceData)
    {
        return new ExecutionResult
        {
            Proceed = false,
            PersistenceData = persistenceData
        };
    }

    protected ExecutionResult SleepResult(object persistenceData, TimeSpan sleep)
    {
        return new ExecutionResult
        {
            Proceed = false,
            PersistenceData = persistenceData,
            SleepFor = sleep
        };
    }
}