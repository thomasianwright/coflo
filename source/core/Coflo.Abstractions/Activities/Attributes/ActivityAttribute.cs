using Coflo.Abstractions.Activities.Enums;

namespace Coflo.Abstractions.Activities.Attributes;

public class ActivityAttribute : Attribute
{
    public string DisplayName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ActivityCategory Category { get; set; } = ActivityCategory.None;
    public string[] Outcomes { get; set; } = { OutcomeNames.Failure, OutcomeNames.Success };
}