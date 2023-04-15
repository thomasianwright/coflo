namespace Coflo.Abstractions.Models.Variable;

public class VariableValue : VariableDefinition
{
    public object? Value { get; set; } = default!;
}