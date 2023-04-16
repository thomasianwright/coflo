using Coflo.Abstractions.Variables.Model;

namespace Coflo.Abstractions.Evaluation.Contracts;

public interface IEvaluationContext
{
    VariableCollection Variables { get; set; }
    public string Condition { get; set; }
}