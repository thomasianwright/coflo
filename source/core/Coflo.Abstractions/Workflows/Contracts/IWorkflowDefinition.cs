using Coflo.Abstractions.Activities.Models;
using Coflo.Abstractions.Connections.Contracts;
using Coflo.Abstractions.Variables.Model;

namespace Coflo.Abstractions.Workflows.Contracts;

public interface IWorkflowDefinition
{
    public long WorkflowDefinitionId { get; }
    public string Name { get; }
    public ICollection<IWorkflowDefinitionVersion> Versions { get; set; }
}