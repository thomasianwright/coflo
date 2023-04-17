using Cassandra;
using Cassandra.Data.Linq;
using Coflo.Abstractions.Workflows.Models;
using Coflo.Infrastructure.Persistance.Cassandra.Factories;

namespace Coflo.Infrastructure.Persistance.Cassandra.Repositories;

internal class CassandraWorkflowDefinitionRepository : IWorkflowDefinitionRepository, IDisposable
{
    private readonly ISession _session;
    private readonly Table<WorkflowDefinitionVersion> _workflowDefinitionVersions;
    private readonly Table<WorkflowDefinition> _workflowDefinitions;

    public CassandraWorkflowDefinitionRepository(ICassandraSessionFactory cassandraSessionFactory)
    {
        _session = cassandraSessionFactory.GetSession();
        _workflowDefinitionVersions = _session.GetTable<WorkflowDefinitionVersion>();
        _workflowDefinitions = _session.GetTable<WorkflowDefinition>();
    }

    public async ValueTask<WorkflowDefinition?> GetWorkflowDefinition(long workflowDefinitionId)
    {
        var result = await _workflowDefinitions
            .FirstOrDefault(x => x.WorkflowDefinitionId == workflowDefinitionId)
            .ExecuteAsync();

        return result;
    }

    public async ValueTask<WorkflowDefinitionVersion?> GetWorkflowDefinitionVersion(long workflowDefinitionId,
        long workflowVersionId)
    {
        var result = await _workflowDefinitionVersions
            .FirstOrDefault(x =>
                x.WorkflowDefinitionId == workflowDefinitionId && x.WorkflowVersionId == workflowVersionId)
            .ExecuteAsync();

        return result;
    }

    public async Task UpdateWorkflowDefinition(WorkflowDefinition workflowDefinition)
    {
        var workflowDef =
            await _workflowDefinitions
                .FirstOrDefault(x => x.WorkflowDefinitionId == workflowDefinition.WorkflowDefinitionId)
                .ExecuteAsync();

        if (workflowDef is null)
        {
            await InsertWorkflowDefinition(workflowDefinition);

            return;
        }

        workflowDef.Name = workflowDefinition.Name;

        var t = _workflowDefinitions
            .UpdateIf(x=> x.WorkflowDefinitionId == workflowDefinition.WorkflowDefinitionId);
    }

    public async Task InsertWorkflowDefinition(WorkflowDefinition workflowDefinition)
    {
        await _workflowDefinitions
            .Insert(workflowDefinition)
            .ExecuteAsync();
    }

    public async Task InsertWorkflowDefinitionVersion(WorkflowDefinitionVersion workflowDefinitionVersion)
    {
        await _workflowDefinitionVersions
            .Insert(workflowDefinitionVersion)
            .ExecuteAsync();
    }

    public async Task RemoveWorkflowDefinition(long workflowDefinitionId)
    {
        await _workflowDefinitions.DeleteIf(x => x.WorkflowDefinitionId == workflowDefinitionId).ExecuteAsync();
        await _workflowDefinitionVersions.DeleteIf(x => x.WorkflowDefinitionId == workflowDefinitionId).ExecuteAsync();
    }

    public void Dispose()
    {
        _session.Dispose();
    }
}