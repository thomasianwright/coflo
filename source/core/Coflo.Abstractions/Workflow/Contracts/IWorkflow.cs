namespace Coflo.Abstractions.Workflow.Contracts;

public interface IWorkflow<TData>
    where TData : new()
{
    long Id { get; }
    int Version { get; }
    
    void Build(IWorkflowBuilder<TData> builder);
}

public interface IWorkflow : IWorkflow<object>
{
}