using Coflo.Abstractions.Connections.Contracts;

namespace Coflo.Abstractions.Activities.Models;

public class ActivityDefinition
{
    public long WorkflowDefinitionId { get; set; }
    public long WorkflowVersionId { get; set; }
    public long ActivityDefinitionId { get; set; }
    public string DisplayName { get; set; }
    public string ActivityName { get; set; }

    public ICollection<ActivityInputMapping> InputMappings { get; set; } = new List<ActivityInputMapping>();
    public ICollection<ActivityOutputMapping> OutputMappings { get; set; } = new List<ActivityOutputMapping>();

    public ICollection<IConnection> InputNode { get; set; } = new List<IConnection>();
    public ICollection<IConnection> OutputNodes { get; set; } = new List<IConnection>();
}