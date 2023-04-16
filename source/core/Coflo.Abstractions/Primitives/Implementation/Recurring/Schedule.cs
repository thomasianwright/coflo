using Coflo.Abstractions.Execution.Models;
using Coflo.Abstractions.Persistence.Models;
using Coflo.Abstractions.Steps.Contracts;

namespace Coflo.Abstractions.Primitives.Recurring;

public class Schedule : ContainerStepBody
{
    public TimeSpan Interval { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (context.PersistenceData == null)
        {
            return ExecutionResult.Sleep(Interval, new SchedulePersistenceData { Elapsed = false });
        }

        if (context.PersistenceData is not SchedulePersistenceData data) throw new ArgumentException();

        if (!data.Elapsed)
            return ExecutionResult.Branch(new List<object> { context.Item },
                new SchedulePersistenceData { Elapsed = true });
        

        return context.Workflow.IsBranchComplete(context.ExecutionPointer.Id)
            ? ExecutionResult.Next()
            : ExecutionResult.Persist(context.PersistenceData);
    }
}