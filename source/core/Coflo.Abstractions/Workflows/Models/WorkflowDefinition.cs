using Coflo.Abstractions.Activities.Models;
using Coflo.Abstractions.Connections.Contracts;
using Coflo.Abstractions.Variables.Model;
using Coflo.Abstractions.Workflows.Contracts;

namespace Coflo.Abstractions.Workflows.Models;

public class WorkflowDefinition : IWorkflowDefinition
{
    public long WorkflowDefinitionId { get; set; }
    public long TenantId { get; set; }
    public string Name { get; set; }
    public ICollection<IWorkflowDefinitionVersion> Versions { get; set; }
}