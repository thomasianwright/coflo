using Coflo.Abstractions.Activities.Models;
using Coflo.Abstractions.Connections.Contracts;
using Coflo.Abstractions.Variables.Model;
using Coflo.Abstractions.Workflows.Contracts;

namespace Coflo.Abstractions.Workflows.Models;

public class WorkflowDefinitionVersion : IWorkflowDefinitionVersion
{
    public long WorkflowVersionId { get; set; }
    public long WorkflowDefinitionId { get; set; }
    public IWorkflowDefinition WorkflowDefinition { get; set; }
    
    public ICollection<IConnection> Connections { get; set; }
    public ICollection<ActivityDefinition> ActivityDefinitions { get; set; }
    public ICollection<VariableDefinition> VariableDefinitions { get; set; }
}