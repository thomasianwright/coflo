using Coflo.Abstractions.Workflows.Commands;
using Coflo.Abstractions.Workflows.Notifications;
using DotNetCore.CAP;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Coflo.Hosting.Orchestrator.CapControllers;

public class WorkflowEventController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkflowEventController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [NonAction]
    [CapSubscribe(nameof(StartWorkflowExecutionCommand))]
    public async Task StartWorkflowExecutionEvent(StartWorkflowExecutionCommand @event,
        CancellationToken cancellationToken = default!)
    {
        await _mediator.Publish(@event, cancellationToken);
    }

    [NonAction]
    [CapSubscribe(nameof(WorkflowExecutionCompletedNotification))]
    public async Task WorkflowExecutionCompletedEvent(WorkflowExecutionCompletedNotification @event,
        CancellationToken cancellationToken = default!)
    {
        await _mediator.Publish(@event, cancellationToken);
    }
    
    [NonAction]
    [CapSubscribe(nameof(WorkflowExecutionFailedNotification))]
    public async Task WorkflowExecutionFailedEvent(WorkflowExecutionFailedNotification @event,
        CancellationToken cancellationToken = default!)
    {
        await _mediator.Publish(@event, cancellationToken);
    }
    
    [NonAction]
    [CapSubscribe(nameof(WorkflowExecutionTerminatedNotification))]
    public async Task WorkflowExecutionTerminatedEvent(WorkflowExecutionTerminatedNotification @event,
        CancellationToken cancellationToken = default!)
    {
        await _mediator.Publish(@event, cancellationToken);
    }
    
    [NonAction]
    [CapSubscribe(nameof(WorkflowExecutionResumedNotification))]
    public async Task WorkflowExecutionResumedEvent(WorkflowExecutionResumedNotification @event,
        CancellationToken cancellationToken = default!)
    {
        await _mediator.Publish(@event, cancellationToken);
    }
}