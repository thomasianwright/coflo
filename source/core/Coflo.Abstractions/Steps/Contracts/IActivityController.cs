using Coflo.Abstractions.Steps.Models;

namespace Coflo.Abstractions.Steps.Contracts;

public interface IActivityController
{
    Task<PendingActivity> GetPendingActivity(string activityName, long workerId, TimeSpan? timeout = null);
    Task ReleaseActivityToken(string token);
    Task SubmitActivitySuccess(string token, object result);
    Task SubmitActivityFailure(string token, object result);
}