using Coflo.Abstractions.Execution.Models;
using Coflo.Abstractions.Steps.Models;

namespace Coflo.Abstractions.Expr.Contracts;

public interface IStepOutcome
{
    string ExternalNextStepId { get; set; }
    string Label { get; set; }
    int NextStep { get; set; }

    bool Matches(object data);
    bool Matches(ExecutionResult executionResult, object data);
}