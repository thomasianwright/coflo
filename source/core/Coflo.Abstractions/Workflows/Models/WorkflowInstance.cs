using Coflo.Abstractions.Variables.Model;
using Coflo.Abstractions.Workflows.Contracts;
using NodaTime;

namespace Coflo.Abstractions.Workflows.Models;

public class WorkflowInstance : IWorkflowInstance
{
    public long InstanceId { get; set; }
    public long WorkflowDefinitionId { get; set; }
    public long WorkflowVersionId { get; set; }
    public long TenantId { get; set; }

    public IVariableCollection Variables { get; set; }
    public List<WorkflowLogEntry> WorkflowLogs { get; set; }
    
    public WorkflowStatus Status { get; set; }
    public Instant CreatedAt { get; set; }
    public Instant? CompletedAt { get; set; }
}