using Coflo.Abstractions.Activities.Contracts;

namespace Coflo.Abstractions.Activities.Models;

public abstract class Activity : IActivity
{
    protected Activity(string name)
    {
        Name = name;
    }
    
    public string Name { get; }

    public abstract Task<IActivityExecutionResult> ExecuteAsync(ActivityExecutionContext context);
}