using System.Data;
using Coflo.Abstractions.Activities.Commands;
using Coflo.Abstractions.Activities.Models;
using Coflo.Abstractions.Activities.Stores;
using Coflo.Abstractions.Evaluation.Contracts;
using Coflo.Abstractions.Events;
using Coflo.Abstractions.Variables.Enums;
using Coflo.Abstractions.Variables.Model;
using Coflo.Activities.Primitives.Control;
using Coflo.Core.Snowflake.Generators;
using Coflo.Hosting.Worker.Commands;
using FluentAssertions;
using Moq;

namespace Coflo.Hosting.Worker.Tests.Commands;

public class StartActivityCommandHandlerTests
{
    public StartActivityCommandHandlerTests()
    {
    }

    [Fact]
    public async Task Input_Mapping_Maps_To_Activity_Properties()
    {
        // Arrange
        var mockActivityDefinitionStore = new Mock<IActivityDefinitionStore>();
        var mockIdGenerator = new Mock<IIdGenerator>();
        var mockServiceProvider = new Mock<IServiceProvider>();
        var mockEventPublisher = new Mock<IEventPublisher>();
        var mockEvaluator = new Mock<IEvaluator>();

        mockEvaluator.Setup(x => x.EvaluateAsync(It.IsAny<IEvaluationContext>()))
            .ReturnsAsync(() => true);

        var command = new StartActivityCommandHandler(mockActivityDefinitionStore.Object, mockIdGenerator.Object,
            mockServiceProvider.Object, mockEventPublisher.Object);

        var variableDef = new VariableDefinition("test", VariableType.String, false, "TEST");
        var variable = new VariableInstance(variableDef);
        variable.SetValue("TEST2");
        
        var variableCollection = new VariableCollection();

        variableCollection.AddOrUpdate(variable);

        var ifActivity = new If(mockEvaluator.Object);
        var activityDef = new ActivityDefinition
        {
            DisplayName = "Test",
            ActivityName = "IF",
            InputMappings = new List<ActivityInputMapping>()
            {
                new ActivityInputMapping()
                {
                    VariableDefinition = variableDef,
                    ActivityInputField = nameof(If.Condition)
                }
            }
        };
        
        // Act

        command.PopulateInputVariablesToProperties(typeof(If), ifActivity, activityDef, variableCollection);

        // Assert
        ifActivity.Condition.Should().Be(variable.Value as string);
    }
    
    [Fact]
    public async Task Input_Mapping_Throws_Constraint_Exception()
    {
        // Arrange
        var mockActivityDefinitionStore = new Mock<IActivityDefinitionStore>();
        var mockIdGenerator = new Mock<IIdGenerator>();
        var mockServiceProvider = new Mock<IServiceProvider>();
        var mockEventPublisher = new Mock<IEventPublisher>();
        var mockEvaluator = new Mock<IEvaluator>();

        mockEvaluator.Setup(x => x.EvaluateAsync(It.IsAny<IEvaluationContext>()))
            .ReturnsAsync(() => true);

        var command = new StartActivityCommandHandler(mockActivityDefinitionStore.Object, mockIdGenerator.Object,
            mockServiceProvider.Object, mockEventPublisher.Object);

        var variableDef = new VariableDefinition("test", VariableType.Boolean, false, false);
        var variable = new VariableInstance(variableDef);
        variable.SetValue(false);
        
        var variableCollection = new VariableCollection();

        variableCollection.AddOrUpdate(variable);

        var ifActivity = new If(mockEvaluator.Object);
        var activityDef = new ActivityDefinition
        {
            DisplayName = "Test",
            ActivityName = "IF",
            InputMappings = new List<ActivityInputMapping>()
            {
                new ActivityInputMapping()
                {
                    VariableDefinition = variableDef,
                    ActivityInputField = nameof(If.Condition)
                }
            }
        };
        
        // Act
        var act = () => command.PopulateInputVariablesToProperties(typeof(If), ifActivity, activityDef, variableCollection);
        
        // Assert
        act.Should().ThrowExactly<ConstraintException>();
    }
}