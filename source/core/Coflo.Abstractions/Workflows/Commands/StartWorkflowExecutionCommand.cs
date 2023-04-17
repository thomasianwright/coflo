using Mediator;

namespace Coflo.Abstractions.Workflows.Commands;

public class StartWorkflowExecutionCommand : ICommand
{
    public long WorkflowInstanceId { get; set; }

    public StartWorkflowExecutionCommand(long workflowInstanceId)
    {
        WorkflowInstanceId = workflowInstanceId;
    }
}