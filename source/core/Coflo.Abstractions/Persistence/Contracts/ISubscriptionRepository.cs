using Coflo.Abstractions.Events.Models;

namespace Coflo.Abstractions.Persistence.Contracts;

public interface ISubscriptionRepository
{        
    Task<string> CreateEventSubscription(EventSubscription subscription, CancellationToken cancellationToken = default);

    Task<IEnumerable<EventSubscription>> GetSubscriptions(string eventName, string eventKey, DateTime asOf, CancellationToken cancellationToken = default);

    Task TerminateSubscription(long eventSubscriptionId, CancellationToken cancellationToken = default);

    Task<EventSubscription> GetSubscription(long eventSubscriptionId, CancellationToken cancellationToken = default);

    Task<EventSubscription> GetFirstOpenSubscription(string eventName, string eventKey, DateTime asOf, CancellationToken cancellationToken = default);
        
    Task<bool> SetSubscriptionToken(long eventSubscriptionId, string token, long workerId, DateTime expiry, CancellationToken cancellationToken = default);
        
    Task ClearSubscriptionToken(long eventSubscriptionId, string token, CancellationToken cancellationToken = default);

}