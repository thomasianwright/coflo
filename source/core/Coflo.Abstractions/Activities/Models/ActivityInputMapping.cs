using Coflo.Abstractions.Variables.Model;

namespace Coflo.Abstractions.Activities.Models;

public class ActivityInputMapping
{
    public string ActivityInputField { get; set; }
    public VariableDefinition? VariableDefinition { get; set; }
    public object? Literal { get; set; }
}