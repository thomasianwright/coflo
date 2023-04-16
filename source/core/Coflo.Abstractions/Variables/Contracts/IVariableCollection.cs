﻿namespace Coflo.Abstractions.Variables.Model;

public interface IVariableCollection
{
    VariableInstance? this[string name] { get; }
    void Add(VariableInstance variable);
    void AddRange(IEnumerable<VariableInstance> variables);
}