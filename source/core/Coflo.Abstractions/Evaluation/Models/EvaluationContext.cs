using Coflo.Abstractions.Evaluation.Contracts;
using Coflo.Abstractions.Variables.Model;

namespace Coflo.Abstractions.Evaluation.Models;

public class EvaluationContext : IEvaluationContext
{
    public IVariableCollection Variables { get; set; }
    public string Condition { get; set; }

    public EvaluationContext(IVariableCollection variables, string condition)
    {
        Variables = variables;
        Condition = condition;
    }
}