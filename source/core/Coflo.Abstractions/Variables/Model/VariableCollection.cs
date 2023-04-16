namespace Coflo.Abstractions.Variables.Model;

public class VariableCollection : IVariableCollection
{
    private readonly IDictionary<string, VariableInstance> _variables;

    public VariableCollection(IDictionary<string, VariableInstance> variables)
    {
        _variables = new Dictionary<string, VariableInstance>(variables);
    }

    public VariableCollection() : this(new Dictionary<string, VariableInstance>())
    {
        
    }
    
    public VariableInstance? this[string name] => _variables[name];
    
    public bool Contains(string name)
    {
        return _variables.ContainsKey(name);
    }
    
    public bool AddOrUpdate(VariableInstance variable)
    {
        if (Contains(variable.Name))
        {
            _variables[variable.Name] = variable;
            return true;
        }

        _variables.Add(variable.Name, variable);
        return false;
    }
    
    public void Add(VariableInstance variable)
    {
        _variables.Add(variable.Name, variable);
    }
    
    public void AddRange(IEnumerable<VariableInstance> variables)
    {
        foreach (var variable in variables)
        {
            Add(variable);
        }
    }
}