namespace Coflo.Abstractions.Activities.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ActivityOutputAttribute : Attribute
{
    public string DisplayName { get; set; }
}