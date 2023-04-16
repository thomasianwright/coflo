using Coflo.Abstractions.Events.Models;
using Coflo.Abstractions.Execution.Models;

namespace Coflo.Abstractions.Workflow.Models;

public class WorkflowResult
{
    public List<EventSubscription> Subscriptions { get; set; } = new();
    public List<ExecutionError> Errors { get; set; } = new();
}