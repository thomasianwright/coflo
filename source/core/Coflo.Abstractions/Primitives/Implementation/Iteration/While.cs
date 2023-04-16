using Coflo.Abstractions.Execution.Models;
using Coflo.Abstractions.Persistence.Exceptions;
using Coflo.Abstractions.Persistence.Models;
using Coflo.Abstractions.Steps.Contracts;

namespace Coflo.Abstractions.Primitives.Iteration;

public class While : ContainerStepBody
{
    public bool Condition { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        switch (context.PersistenceData)
        {
            case null when Condition:
                return ExecutionResult.Branch(new List<object> { context.Item },
                    new ControlPersistenceData { ChildrenActive = true });
            case null:
                return ExecutionResult.Next();
            case ControlPersistenceData data when (data.ChildrenActive):
            {
                return ExecutionResult.Persist(!context.Workflow.IsBranchComplete(context.ExecutionPointer.Id)
                    ? context.PersistenceData
                    : null); //re-evaluate condition on next pass
            }
            default:
                throw new CorruptPersistenceDataException();
        }
    }
}