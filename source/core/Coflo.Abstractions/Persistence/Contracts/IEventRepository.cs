﻿using Coflo.Abstractions.Events.Models;

namespace Coflo.Abstractions.Persistence.Contracts;

public interface IEventRepository
{
    Task<string> CreateEvent(Event newEvent, CancellationToken cancellationToken = default);

    Task<Event> GetEvent(string id, CancellationToken cancellationToken = default);

    Task<IEnumerable<string>> GetRunnableEvents(DateTime asAt, CancellationToken cancellationToken = default);

    Task<IEnumerable<string>> GetEvents(string eventName, string eventKey, DateTime asOf,
        CancellationToken cancellationToken = default);

    Task MarkEventProcessed(string id, CancellationToken cancellationToken = default);

    Task MarkEventUnprocessed(string id, CancellationToken cancellationToken = default);
}