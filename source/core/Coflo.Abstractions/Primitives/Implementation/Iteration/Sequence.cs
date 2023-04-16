using Coflo.Abstractions.Execution.Models;
using Coflo.Abstractions.Persistence.Exceptions;
using Coflo.Abstractions.Persistence.Models;
using Coflo.Abstractions.Steps.Contracts;

namespace Coflo.Abstractions.Primitives.Iteration;

public class Sequence : ContainerStepBody
{
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        return context.PersistenceData switch
        {
            null => ExecutionResult.Branch(new List<object> { context.Item },
                new ControlPersistenceData { ChildrenActive = true }),
            ControlPersistenceData { ChildrenActive: true } =>
                context.Workflow.IsBranchComplete(context.ExecutionPointer.Id)
                    ? ExecutionResult.Next()
                    : ExecutionResult.Persist(context.PersistenceData),
            _ => throw new CorruptPersistenceDataException()
        };
    }
}