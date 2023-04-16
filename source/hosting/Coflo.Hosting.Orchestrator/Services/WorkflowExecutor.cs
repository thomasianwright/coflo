using Ardalis.GuardClauses;
using Coflo.Abstractions.Activities.Commands;
using Coflo.Abstractions.Activities.Stores;
using Coflo.Abstractions.Events;
using Coflo.Abstractions.Variables.Model;
using Coflo.Abstractions.Workflows;
using Coflo.Abstractions.Workflows.Commands;
using Coflo.Abstractions.Workflows.Models;
using Coflo.Abstractions.Workflows.Notifications;
using Coflo.Abstractions.Workflows.Services;
using Coflo.Abstractions.Workflows.Stores;
using Coflo.Core.Snowflake.Generators;
using NodaTime;

namespace Coflo.Hosting.Orchestrator.Services;

public class WorkflowExecutor : IWorkflowExecutor
{
    private readonly IWorkflowDefinitionStore _workflowDefinitionStore;
    private readonly IWorkflowInstanceStore _workflowInstanceStore;
    private readonly IActivityDefinitionStore _activityDefinitionStore;
    private readonly IClock _clock;
    private readonly IIdGenerator _idGenerator;
    private readonly IEventPublisher _eventPublisher;

    public WorkflowExecutor(IWorkflowDefinitionStore workflowDefinitionStore,
        IWorkflowInstanceStore workflowInstanceStore, IActivityDefinitionStore activityDefinitionStore, IClock clock,
        IIdGenerator idGenerator,
        IEventPublisher eventPublisher)
    {
        _workflowDefinitionStore = workflowDefinitionStore;
        _workflowInstanceStore = workflowInstanceStore;
        _activityDefinitionStore = activityDefinitionStore;
        _clock = clock;
        _idGenerator = idGenerator;
        _eventPublisher = eventPublisher;
    }

    public async Task<long> InitializeWorkflow(long workflowDefinitionId, long workflowVersionId,
        VariableCollection variables)
    {
        var definition = await _workflowDefinitionStore.GetWorkflowDefinitionAsync(workflowDefinitionId);
        var time = _clock.GetCurrentInstant();

        var instance = new WorkflowInstance
        {
            WorkflowDefinitionId = workflowDefinitionId,
            WorkflowVersionId = workflowVersionId,
            Variables = variables,
            Status = WorkflowStatus.Created,
            CreatedAt = time
        };

        instance.InstanceId = await _idGenerator.NextId();

        await _workflowInstanceStore.SaveWorkflowInstanceAsync(instance);

        await _eventPublisher.PublishAsync(new StartWorkflowExecutionCommand(instance.InstanceId));

        return instance.InstanceId;
    }

    public async Task ExecuteWorkflow(long workflowInstanceId)
    {
        var workflowInstance = await _workflowInstanceStore.GetWorkflowInstanceAsync(workflowInstanceId);
        var version =
            await _workflowDefinitionStore.GetWorkflowDefinitionVersionAsync(workflowInstance.WorkflowDefinitionId,
                workflowInstance.WorkflowVersionId);

        var startActivity = version.ActivityDefinitions.FirstOrDefault(x => x.ActivityName == "START");

        Guard.Against.Null(startActivity, nameof(startActivity));

        var firstConnection =
            version.Connections.FirstOrDefault(x => x.ActivityId == startActivity.ActivityDefinitionId);

        Guard.Against.Null(firstConnection, nameof(firstConnection));

        var nextActivity =
            version.ActivityDefinitions.FirstOrDefault(x => x.ActivityDefinitionId == firstConnection.TargetActivityId);

        Guard.Against.Null(nextActivity, nameof(nextActivity));

        var activityDefinition =
            await _activityDefinitionStore.GetActivityDefinitionAsync(nextActivity.ActivityDefinitionId);

        await _eventPublisher.PublishAsync(new StartActivityCommand(workflowInstanceId,
            activityDefinition.ActivityDefinitionId));

        workflowInstance.Status = WorkflowStatus.Running;

        await _workflowInstanceStore.SaveWorkflowInstanceAsync(workflowInstance);
    }

    public async Task ActivityCompleted(ActivityCompletedNotification completedNotification)
    {
        var workflowInstance =
            await _workflowInstanceStore.GetWorkflowInstanceAsync(completedNotification.WorkflowInstanceId);

        var version = await _workflowDefinitionStore.GetWorkflowDefinitionVersionAsync(
            workflowInstance.WorkflowDefinitionId,
            workflowInstance.WorkflowVersionId);

        workflowInstance.WorkflowLogs.Add(new WorkflowLogEntry(workflowInstance.InstanceId, "Activity completed, {0}",
            _clock.GetCurrentInstant()));
        
        var nextConnection =
            version.Connections.FirstOrDefault(x => x.ActivityId == completedNotification.NodeId && x.Outcome == completedNotification.Outcome);

        if (nextConnection == null)
        {
            await WorkflowCompleted(workflowInstance.InstanceId);
            return;
        }

        await _eventPublisher.PublishAsync(new StartActivityCommand(workflowInstance.InstanceId,
            nextConnection.TargetActivityId));
    }

    public Task ActivityFailed(ActivityFailedNotification failedNotification)
    {
        throw new NotImplementedException();
    }

    public Task RunNextActivity(long workflowInstanceId, long activityDefinitionId)
    {
        throw new NotImplementedException();
    }

    public Task WorkflowCompleted(long workflowInstanceId)
    {
        throw new NotImplementedException();
    }
}