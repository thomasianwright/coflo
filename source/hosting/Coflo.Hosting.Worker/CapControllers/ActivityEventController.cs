using Coflo.Abstractions.Activities.Commands;
using DotNetCore.CAP;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Coflo.Hosting.Worker.CapControllers;

public class ActivityEventController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActivityEventController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [NonAction]
    [CapSubscribe(nameof(StartActivityCommand))]
    public async Task StartActivityEvent(StartActivityCommand command, CancellationToken cancellationToken = default!)
    {
        await _mediator.Publish(command, cancellationToken);
    }
}