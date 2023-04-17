namespace Coflo.Abstractions.Activities.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ActivityInputAttribute : Attribute
{
    public string DisplayName { get; set; }
}