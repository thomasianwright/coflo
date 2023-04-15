using Coflo.Abstractions.Contracts.Tenant;
using NodaTime;

namespace Coflo.Abstractions.Models.Workflow;

public class WorkflowDefinition : ITenantScope
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public Guid? TenantId { get; set; }

    public ICollection<WorkflowDefinitionVersion> Versions { get; set; }
    
    public Instant CreatedAt { get; set; }
    public Instant UpdatedAt { get; set; }
}