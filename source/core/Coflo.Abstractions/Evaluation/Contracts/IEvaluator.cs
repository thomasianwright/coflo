namespace Coflo.Abstractions.Evaluation.Contracts;

public interface IEvaluator
{
    Task<bool> EvaluateAsync(IEvaluationContext context);
}