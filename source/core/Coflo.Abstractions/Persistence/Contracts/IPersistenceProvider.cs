using Coflo.Abstractions.Execution.Models;

namespace Coflo.Abstractions.Persistence.Contracts;

public interface IPersistenceProvider : IWorkflowRepository, ISubscriptionRepository, IEventRepository,
    IScheduledCommandRepository
{
    Task PersistErrors(IEnumerable<ExecutionError> errors, CancellationToken cancellationToken = default);

    void EnsureStoreExists();
}