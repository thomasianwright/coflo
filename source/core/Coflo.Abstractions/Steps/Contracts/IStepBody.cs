using Coflo.Abstractions.Execution.Models;
using Coflo.Abstractions.Steps.Models;

namespace Coflo.Abstractions.Steps.Contracts;

public interface IStepBody
{        
    Task<ExecutionResult> RunAsync(IStepExecutionContext context);        
}