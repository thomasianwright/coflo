using Coflo.Abstractions.Events.Models;

namespace Coflo.Abstractions.Persistence.Contracts;

public interface IScheduledCommandRepository
{
    bool SupportsScheduledCommands { get; }

    Task ScheduleCommand(ScheduledCommand command);

    Task ProcessCommands(DateTimeOffset asOf, Func<ScheduledCommand, Task> action, CancellationToken cancellationToken = default);
}