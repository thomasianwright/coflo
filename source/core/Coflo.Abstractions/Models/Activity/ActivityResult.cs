namespace Coflo.Abstractions.Models.Activity;

public class ActivityResult
{
    public List<string> ActivityLog { get; set; } = new();
    public bool IsSuccess { get; set; }
    public string? Outcome { get; set; }

    public ActivityResult(bool isSuccess = false, List<string> activityLog = null, string? outcome = null)
    {
        IsSuccess = isSuccess;
        ActivityLog = activityLog ?? new List<string>();
        Outcome = outcome;
    }
}