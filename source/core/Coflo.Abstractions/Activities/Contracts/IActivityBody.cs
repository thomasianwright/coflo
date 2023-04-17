using Coflo.Abstractions.Activities.Models;

namespace Coflo.Abstractions.Activities.Contracts;

public interface IActivityBody
{
    Task<IActivityExecutionResult> ExecuteAsync(ActivityExecutionContext context);
}