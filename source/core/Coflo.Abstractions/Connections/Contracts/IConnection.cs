using Coflo.Abstractions.Activities;

namespace Coflo.Abstractions.Connections.Contracts;

public class IConnection
{
    public long ActivityId { get; set; }
    public long TargetActivityId { get; set; }
    public string Outcome { get; set; } = OutcomeNames.Success;
}