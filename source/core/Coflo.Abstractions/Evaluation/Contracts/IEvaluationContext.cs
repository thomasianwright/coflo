using Coflo.Abstractions.Variables.Model;

namespace Coflo.Abstractions.Evaluation.Contracts;

public interface IEvaluationContext
{
    IVariableCollection Variables { get; set; }
    public string Condition { get; set; }
}