using System.Data;
using System.Reflection;
using Ardalis.GuardClauses;
using Coflo.Abstractions.Activities.Attributes;
using Coflo.Abstractions.Activities.Commands;
using Coflo.Abstractions.Activities.Contracts;
using Coflo.Abstractions.Activities.Models;
using Coflo.Abstractions.Activities.Stores;
using Coflo.Abstractions.Events;
using Coflo.Abstractions.Variables.Model;
using Coflo.Abstractions.Workflows.Notifications;
using Coflo.Core.Snowflake.Generators;
using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace Coflo.Hosting.Worker.Commands;

public class StartActivityCommandHandler : ICommandHandler<StartActivityCommand>
{
    private readonly IActivityDefinitionStore _activityDefinitionStore;
    private readonly IIdGenerator _idGenerator;
    private readonly IServiceProvider _serviceProvider;
    private readonly IEventPublisher _publisher;

    public StartActivityCommandHandler(IActivityDefinitionStore activityDefinitionStore, IIdGenerator idGenerator,
        IServiceProvider serviceProvider, IEventPublisher publisher)
    {
        _activityDefinitionStore = activityDefinitionStore;
        _idGenerator = idGenerator;
        _serviceProvider = serviceProvider;
        _publisher = publisher;
    }

    public async ValueTask<Unit> Handle(StartActivityCommand command, CancellationToken cancellationToken)
    {
        var activityDef = await _activityDefinitionStore.GetActivityDefinitionAsync(command.ActivityDefinitionId);

        var activityType = Type.GetType(activityDef.ActivityName, true, true);

        Guard.Against.Null(activityType);

        var activity = (IActivity)_serviceProvider.GetRequiredService(activityType);

        var executionContext = new ActivityExecutionContext(command.VariableCollection, command.WorkflowInstanceId,
            await _idGenerator.NextId(), activityDef.ActivityName);

        PopulateInputVariablesToProperties(activityType, activity, activityDef, command.VariableCollection);
        PopulateOutputVariablesToProperties(activityType, activity, activityDef, command.VariableCollection);

        var result = await activity.ExecuteAsync(executionContext);

        await _publisher.PublishAsync(new ActivityCompletedNotification
        {
            Variables = result.VariableCollection,
            WorkflowInstanceId = command.WorkflowInstanceId,
            Outcome = result.Outcome,
            NodeId = command.ActivityDefinitionId
        });

        return Unit.Value;
    }

    internal void PopulateInputVariablesToProperties(Type activityType, IActivity activity,
        ActivityDefinition activityDefinition,
        IVariableCollection variables)
    {
        var properties = activityType.GetProperties();

        foreach (var property in properties.Where(x =>
                     activityDefinition.InputMappings.Any(y => y.ActivityInputField == x.Name) &&
                     x.GetCustomAttribute<ActivityInputAttribute>() != null))
        {
            var input = activityDefinition.InputMappings.FirstOrDefault(x => x.ActivityInputField == property.Name);

            if (input?.VariableDefinition?.Name == null) continue;

            var variable = variables[input.VariableDefinition.Name];

            if (variable?.Value.GetType() != property.PropertyType)
                throw new ConstraintException("Variable type does not match property type");

            property.SetValue(activity, variable.Value);
        }
    }

    internal void PopulateOutputVariablesToProperties(Type activityType, IActivity activity,
        ActivityDefinition activityDefinition,
        IVariableCollection variables)
    {
        var properties = activityType.GetProperties();

        foreach (var property in properties)
        {
            var outputAttribute = property.GetCustomAttribute<ActivityOutputAttribute>();

            if (outputAttribute == null) continue;
            var output = activityDefinition.OutputMappings.FirstOrDefault(x => x.ActivityOutputField == property.Name);

            if (string.IsNullOrEmpty(output?.VariableDefinition?.Name)) continue;

            var variable = variables[output.VariableDefinition.Name];

            if (variable?.GetType() != property.GetType())
                throw new ConstraintException("Variable type does not match property type");

            var value = property.GetValue(activity);

            variable.SetValue(value);

            variables.AddOrUpdate(variable);
        }
    }
}