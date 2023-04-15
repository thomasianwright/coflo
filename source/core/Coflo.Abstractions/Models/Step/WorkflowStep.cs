namespace Coflo.Abstractions.Models.Step;

public class WorkflowStep
{
    public string Name { get; set; } = default!;
    public string DisplayName { get; set; } = default!;

    public string ActivityName { get; set; } = default!;

    public string? Condition { get; set; }
    public ICollection<WorkflowStep> NextSteps { get; set; }
}