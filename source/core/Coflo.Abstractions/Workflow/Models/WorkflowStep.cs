using System.Linq.Expressions;
using Coflo.Abstractions.Execution.Models;
using Coflo.Abstractions.Expr.Contracts;
using Coflo.Abstractions.Steps.Contracts;
using Coflo.Abstractions.Steps.Models;
using Coflo.Abstractions.Workflow.Contracts;
using Coflo.Abstractions.Workflow.Enums;

namespace Coflo.Abstractions.Workflow.Models;

public abstract class WorkflowStep
{
    public abstract Type BodyType { get; }
    public virtual int Id { get; set; }

    public virtual string Name { get; set; }

    public virtual string ExternalId { get; set; }

    public virtual List<int> Children { get; set; } = new();

    public virtual List<IStepOutcome> Outcomes { get; set; } = new();

    public virtual List<IStepParameter> Inputs { get; set; } = new();

    public virtual List<IStepParameter> Outputs { get; set; } = new();

    public virtual WorkflowErrorHandling? ErrorBehavior { get; set; }

    public virtual TimeSpan? RetryInterval { get; set; }

    public virtual int? CompensationStepId { get; set; }

    public virtual bool ResumeChildrenAfterCompensation => true;

    public virtual bool RevertChildrenAfterCompensation => false;

    public virtual LambdaExpression CancelCondition { get; set; }

    public bool ProceedOnCancel { get; set; } = false;

    public virtual ExecutionPipelineDirective InitForExecution(WorkflowResult result,
        WorkflowDefinition definition, WorkflowInstance workflow, ExecutionPointer executionPointer)
    {
        return ExecutionPipelineDirective.Next;
    }

    public virtual ExecutionPipelineDirective BeforeExecute(WorkflowResult result,
        IStepExecutionContext context, ExecutionPointer executionPointer, IStepBody body)
    {
        return ExecutionPipelineDirective.Next;
    }

    public virtual void AfterExecute(WorkflowResult result, IStepExecutionContext context,
        ExecutionResult stepResult, ExecutionPointer executionPointer)
    {
    }

    public virtual void PrimeForRetry(ExecutionPointer pointer)
    {
    }

    /// <summary>
    /// Called after every workflow execution round,
    /// every exectuon pointer with no end time, even if this step was not executed in this round
    /// </summary>
    /// <param name="result"></param>
    /// <param name="defintion"></param>
    /// <param name="workflow"></param>
    /// <param name="executionPointer"></param>
    public virtual void AfterWorkflowIteration(WorkflowResult result, WorkflowDefinition defintion,
        WorkflowInstance workflow, ExecutionPointer executionPointer)
    {
    }

    public virtual IStepBody ConstructBody(IServiceProvider serviceProvider)
    {
        IStepBody body = (serviceProvider.GetService(BodyType) as IStepBody);
        if (body == null)
        {
            var stepCtor = BodyType.GetConstructor(new Type[] { });
            if (stepCtor != null)
                body = (stepCtor.Invoke(null) as IStepBody);
        }

        return body;
    }
}

public class WorkflowStep<TStepBody> : WorkflowStep
    where TStepBody : IStepBody
{
    public override Type BodyType => typeof(TStepBody);
}