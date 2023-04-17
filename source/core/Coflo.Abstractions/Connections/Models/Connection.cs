using Coflo.Abstractions.Activities;
using Coflo.Abstractions.Connections.Contracts;

namespace Coflo.Abstractions.Connections.Models;

public class Connection : IConnection
{
    public long ActivityId { get; set; }
    public long TargetActivityId { get; set; }
    public string Outcome { get; set; } = OutcomeNames.Success;
}