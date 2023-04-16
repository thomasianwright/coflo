using Coflo.Abstractions.Activities.Contracts;
using Coflo.Abstractions.Connections.Contracts;
using Coflo.Abstractions.Variables.Model;
using Coflo.Abstractions.Workflows.Contracts;
using Coflo.Abstractions.Workflows.Stores;

namespace Coflo.Abstractions.Activities.Services;

public interface IActivityExecutor
{
    Task ExecuteAsync(IActivity activity, VariableCollection variables, long workflowInstanceId, long nodeId);
}