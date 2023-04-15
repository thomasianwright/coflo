using Coflo.Abstractions.Contracts.Tenant;
using NodaTime;

namespace Coflo.Abstractions.Models.Workflow;

public class WorkflowDefinitionVersion : ITenantScope
{
    public long WorkflowId { get; set; }
    public long VersionId { get; set; }
    public Guid? TenantId { get; set; }

    
    
    public Instant CreatedAt { get; set; }
    public Instant UpdatedAt { get; set; }
}