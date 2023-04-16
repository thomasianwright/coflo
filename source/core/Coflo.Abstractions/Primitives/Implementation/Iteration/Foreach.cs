using System.Collections;
using Coflo.Abstractions.Execution.Models;
using Coflo.Abstractions.Persistence.Models;
using Coflo.Abstractions.Steps.Contracts;

namespace Coflo.Abstractions.Primitives.Iteration;

public class Foreach : ContainerStepBody
{
    public IEnumerable Collection { get; set; }
    public bool RunParallel { get; set; } = true;

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        switch (context.PersistenceData)
        {
            case null:
            {
                var values = Collection.Cast<object>().ToList();

                if (!values.Any())
                    return ExecutionResult.Next();

                return ExecutionResult.Branch(
                    RunParallel ? new List<object>(values) : new List<object>(new object[] { values.ElementAt(0) }),
                    new IteratorPersistenceData { ChildrenActive = true });
            }
            case IteratorPersistenceData { ChildrenActive: true } persistenceData:
            {
                if (!context.Workflow.IsBranchComplete(context.ExecutionPointer.Id))
                    return ExecutionResult.Persist(persistenceData);

                if (RunParallel) return ExecutionResult.Next();

                var values = Collection.Cast<object>();

                persistenceData.Index++;

                return persistenceData.Index < values.Count()
                    ? ExecutionResult.Branch(new List<object>(new object[] { values.ElementAt(persistenceData.Index) }),
                        persistenceData)
                    : ExecutionResult.Next();
            }
        }

        if (context.PersistenceData is not ControlPersistenceData { ChildrenActive: true })
            return ExecutionResult.Persist(context.PersistenceData);

        return context.Workflow.IsBranchComplete(context.ExecutionPointer.Id)
            ? ExecutionResult.Next()
            : ExecutionResult.Persist(context.PersistenceData);
    }
}