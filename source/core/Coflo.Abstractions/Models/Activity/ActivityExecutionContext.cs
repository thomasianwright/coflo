using Coflo.Abstractions.Models.Variable;

namespace Coflo.Abstractions.Models.Activity;

public class ActivityExecutionContext
{
    public List<VariableValue> Variables { get; set; } = new();
    public List<VariableDefinition> PersistDefinitions { get; set; } = new();
    
    public string Name { get; set; } = default!;
    public string ActivityName { get; set; } = default!;
    public string CorrelationId { get; set; } = default!;
    public long WorkflowInstanceId { get; set; }
    public long WorkflowDefinitionId { get; set; }
}