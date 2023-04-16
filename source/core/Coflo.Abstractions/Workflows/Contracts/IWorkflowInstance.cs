using Coflo.Abstractions.Connections.Contracts;
using Coflo.Abstractions.Variables.Model;
using Coflo.Abstractions.Workflows.Models;
using NodaTime;

namespace Coflo.Abstractions.Workflows.Contracts;

public interface IWorkflowInstance
{
    public long InstanceId { get; set; }
    public long WorkflowDefinitionId { get; set; }
    public long WorkflowVersionId { get; set; }

    public IVariableCollection Variables { get; set; }
    List<WorkflowLogEntry> WorkflowLogs { get; set; }
    public WorkflowStatus Status { get; set; }

    public Instant CreatedAt { get; set; }
    public Instant? CompletedAt { get; set; }
}