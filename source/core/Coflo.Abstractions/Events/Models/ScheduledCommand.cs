using Mediator;

namespace Coflo.Abstractions.Events.Models;

public class ScheduledCommand : ICommand
{
    public const string ProcessWorkflow = "ProcessWorkflow";
    public const string ProcessEvent = "ProcessEvent";

    public string CommandName { get; set; }
    public string Data { get; set; }
    public long ExecuteTime { get; set; }
}