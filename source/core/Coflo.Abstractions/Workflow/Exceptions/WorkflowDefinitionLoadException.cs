namespace Coflo.Abstractions.Workflow.Exceptions;

public class WorkflowDefinitionLoadException : Exception
{
    public WorkflowDefinitionLoadException(string message)
        : base(message)
    {
    }
}