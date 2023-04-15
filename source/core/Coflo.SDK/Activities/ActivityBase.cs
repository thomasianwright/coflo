using Coflo.Abstractions.Models.Activity;

namespace Coflo.SDK.Activities;

public abstract class ActivityBase : ActivityDefinition
{
    public abstract ActivityResult ExecuteActivityAsync(ActivityExecutionContext context);
}