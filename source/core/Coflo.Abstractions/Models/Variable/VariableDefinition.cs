using Coflo.Abstractions.Enums;

namespace Coflo.Abstractions.Models.Variable;

public class VariableDefinition
{
    public string Name { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public VariableType Type { get; set; } = default!;
    public bool IsArray { get; set; }
    public object? DefaultValue { get; set; }
    public bool IsRequired { get; set; }
    public bool IsSecret { get; set; }
    public string? Description { get; set; }
    public string? Validation { get; set; }
}