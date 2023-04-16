namespace Coflo.Abstractions.Primitives.Models;

public class ActivityResult
{
    public enum StatusType { Success, Fail }
    public StatusType Status { get; set; }
    public long SubscriptionId { get; set; }
    public object Data { get; set; }
}