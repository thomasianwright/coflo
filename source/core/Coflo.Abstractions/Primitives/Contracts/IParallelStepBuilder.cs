using Coflo.Abstractions.Primitives.Iteration;
using Coflo.Abstractions.Steps.Contracts;
using Coflo.Abstractions.Workflow.Contracts;

namespace Coflo.Abstractions.Primitives.Contracts;

public interface IParallelStepBuilder<TData, TStepBody>
    where TStepBody : IStepBody
{
    IParallelStepBuilder<TData, TStepBody> Do(Action<IWorkflowBuilder<TData>> builder);
    IStepBuilder<TData, Sequence> Join();
}