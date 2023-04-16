using Coflo.Abstractions.Events;
using DotNetCore.CAP;
using Mediator;

namespace Coflo.Core.Services;

public class CapEventPublisher : IEventPublisher
{
    private readonly ICapPublisher _capPublisher;

    public CapEventPublisher(ICapPublisher capPublisher)
    {
        _capPublisher = capPublisher;
    }


    public Task PublishAsync<T>(T @event) where T : IMessage
        => _capPublisher.PublishAsync(nameof(@event), @event);
}