namespace Coflo.Abstractions.Models.Activity;

public class ActivityDefinition
{
    public virtual string DisplayName { get; set; } = default!;
    public virtual string? Description { get; set; }

    public virtual List<string> Outcomes { get; set; } = new()
    {
        "Success",
        "Failure"
    };
}