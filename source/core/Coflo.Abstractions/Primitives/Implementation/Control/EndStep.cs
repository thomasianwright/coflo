using Coflo.Abstractions.Workflow.Enums;
using Coflo.Abstractions.Workflow.Models;

namespace Coflo.Abstractions.Primitives.Control;

public class EndStep : WorkflowStep
{
    public override Type BodyType => null!;

    public override ExecutionPipelineDirective InitForExecution(WorkflowResult result, WorkflowDefinition definition,
        WorkflowInstance workflow, ExecutionPointer executionPointer)
    {
        return ExecutionPipelineDirective.EndWorkflow;
    }
}