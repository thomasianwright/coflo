using Coflo.Abstractions.Variables.Enums;

namespace Coflo.Abstractions.Variables.Model;

public class VariableInstance : VariableDefinition
{
    public VariableInstance(VariableDefinition definition) : base(definition)
    {
        
    }
    
    public object? Value { get; private set; }
    
    public void SetValue(object? value)
    {
        if (value == null)
        {
            Value = null;
            return;
        }
        
        switch (value)
        {
            case string when VariableType == VariableType.String:
                Value = value;
                return;
            case int when VariableType == VariableType.Number:
                Value = value;
                return;
            case bool when VariableType == VariableType.Boolean:
                Value = value;
                return;
        }
        
        if (value.GetType() != typeof(object) || VariableType != VariableType.Object) return;

        Value = value;
    }
    
    public bool ValidateValue(object? value)
    {
        if (value == null)
        {
            return true;
        }
        
        switch (value)
        {
            case string when VariableType == VariableType.String:
                return true;
            case int when VariableType == VariableType.Number:
                return true;
            case bool when VariableType == VariableType.Boolean:
                return true;
        }
        
        if (value.GetType() != typeof(object) || VariableType != VariableType.Object) return false;

        return true;
    }
}