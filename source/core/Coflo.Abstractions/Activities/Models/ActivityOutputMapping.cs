using Coflo.Abstractions.Variables.Model;

namespace Coflo.Abstractions.Activities.Models;

public class ActivityOutputMapping
{
    public string ActivityOutputField { get; set; }
    public VariableDefinition Variable { get; set; }
}