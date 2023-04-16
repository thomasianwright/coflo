using Coflo.Abstractions.Variables.Enums;

namespace Coflo.Abstractions.Variables.Model;

public class VariableDefinition
{
    public string Name { get; set; } = null!;
    public VariableType VariableType { get; set; } = VariableType.String;
    public bool IsArray { get; set; }
    public object? DefaultValue { get; private set; }
    public bool Nullable { get; set; }
    public bool Persist { get; set; } = true;
    public VariableDefinition(string name, VariableType variableType, bool isArray, object? defaultValue)
    {
        Name = name;
        VariableType = variableType;
        IsArray = isArray;

        if (SetDefaultValue(defaultValue))
            throw new ArgumentException($"Invalid default value for variable type {variableType}");
    }

    public VariableDefinition(VariableDefinition variableDefinition) : this(variableDefinition.Name,
        variableDefinition.VariableType, variableDefinition.IsArray, variableDefinition.DefaultValue)
    {
    }

    private bool SetDefaultValue(object? value)
    {
        switch (value)
        {
            case null:
                DefaultValue = null;
                return true;
            case string when VariableType == VariableType.String:
                DefaultValue = value;
                return true;
            case int when VariableType == VariableType.Number:
                DefaultValue = value;
                return true;
            case bool when VariableType == VariableType.Boolean:
                DefaultValue = value;
                return true;
        }

        if (value.GetType() != typeof(object) || VariableType != VariableType.Object) return false;

        DefaultValue = value;

        return true;
    }
}