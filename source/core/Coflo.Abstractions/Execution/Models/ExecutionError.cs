namespace Coflo.Abstractions.Execution.Models;

public class ExecutionError
{
    public DateTime ErrorTime { get; set; }

    public long WorkflowId { get; set; }

    public long ExecutionPointerId { get; set; }

    public string Message { get; set; }
}