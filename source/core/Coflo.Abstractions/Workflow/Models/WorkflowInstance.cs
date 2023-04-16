using Coflo.Abstractions.Workflow.Enums;

namespace Coflo.Abstractions.Workflow.Models;

public class WorkflowInstance
{
    public long Id { get; set; }

    public string WorkflowDefinitionId { get; set; }

    public int Version { get; set; }

    public string Description { get; set; }

    public string Reference { get; set; }

    public ExecutionPointerCollection ExecutionPointers { get; set; } = new();

    public long? NextExecution { get; set; }

    public WorkflowStatus Status { get; set; }

    public object Data { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime? CompleteTime { get; set; }

    public bool IsBranchComplete(string parentId)
    {
        return ExecutionPointers
            .FindByScope(parentId)
            .All(x => x.EndTime != null);
    }
}