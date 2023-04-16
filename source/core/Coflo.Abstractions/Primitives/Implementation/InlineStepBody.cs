using Coflo.Abstractions.Execution.Models;
using Coflo.Abstractions.Steps.Contracts;
using Coflo.Abstractions.Steps.Models;

namespace Coflo.Abstractions.Primitives;

public class InlineStepBody : StepBody
{

    private readonly Func<IStepExecutionContext, ExecutionResult> _body;

    public InlineStepBody(Func<IStepExecutionContext, ExecutionResult> body)
    {
        _body = body;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        return _body.Invoke(context);
    }
}