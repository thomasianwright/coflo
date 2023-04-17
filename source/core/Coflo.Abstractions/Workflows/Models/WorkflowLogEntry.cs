namespace Coflo.Abstractions.Workflows.Models;

public class WorkflowLogEntry
{
    public long WorkflowInstanceId { get; set; }
    public string Message { get; set; }
    public object[] Params { get; set; }
    
    public WorkflowLogEntry(long workflowInstanceId, string message, params object[] @params)
    {
        WorkflowInstanceId = workflowInstanceId;
        Message = message;
        Params = @params;
    }
    
    public override string ToString()
    {
        return string.Format(Message, Params);
    }
}