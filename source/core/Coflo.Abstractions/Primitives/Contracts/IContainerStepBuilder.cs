using Coflo.Abstractions.Steps.Contracts;
using Coflo.Abstractions.Workflow.Contracts;

namespace Coflo.Abstractions.Primitives.Contracts;

public interface IContainerStepBuilder<TData, TStepBody, TReturnStep>
    where TStepBody : IStepBody
    where TReturnStep : IStepBody
{
    /// <summary>
    /// The block of steps to execute
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    IStepBuilder<TData, TReturnStep> Do(Action<IWorkflowBuilder<TData>> builder);
}