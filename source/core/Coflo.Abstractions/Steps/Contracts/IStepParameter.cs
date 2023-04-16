namespace Coflo.Abstractions.Steps.Contracts;

public interface IStepParameter
{
    void AssignInput(object data, IStepBody body, IStepExecutionContext context);
    void AssignOutput(object data, IStepBody body, IStepExecutionContext context);
}