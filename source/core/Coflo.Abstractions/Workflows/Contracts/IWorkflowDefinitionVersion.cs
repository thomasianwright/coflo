using Coflo.Abstractions.Activities.Models;
using Coflo.Abstractions.Connections.Contracts;
using Coflo.Abstractions.Variables.Model;

namespace Coflo.Abstractions.Workflows.Contracts;

public interface IWorkflowDefinitionVersion
{
    public long WorkflowVersionId { get; }
    public long WorkflowDefinitionId { get; }
    public IWorkflowDefinition WorkflowDefinition { get; }

    public ICollection<IConnection> Connections { get; set; }
    public ICollection<ActivityDefinition> ActivityDefinitions { get; set; }
    public ICollection<VariableDefinition> VariableDefinitions { get; set; }
}