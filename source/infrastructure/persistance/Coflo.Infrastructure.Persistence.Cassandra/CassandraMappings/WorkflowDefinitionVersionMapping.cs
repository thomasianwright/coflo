using Cassandra.Mapping;
using Coflo.Abstractions.Workflows.Models;

namespace Coflo.Infrastructure.Persistance.Cassandra.CassandraMappings;

public class WorkflowDefinitionVersionMapping : Mappings
{
    public WorkflowDefinitionVersionMapping()
    {
        For<WorkflowDefinitionVersion>()
            .TableName("workflow_definition_version")
            .PartitionKey(x => x.TenantId)
            .ClusteringKey(x => x.WorkflowDefinitionId);
    }
}