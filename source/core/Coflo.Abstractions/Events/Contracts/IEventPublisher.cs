using Mediator;

namespace Coflo.Abstractions.Events;

public interface IEventPublisher
{
    Task PublishAsync<T>(T @event) where T : IMessage;
}