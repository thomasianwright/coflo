using Cassandra.Mapping;
using Coflo.Abstractions.Workflows.Models;

namespace Coflo.Infrastructure.Persistance.Cassandra.CassandraMappings;

public class WorkflowDefinitionMapping : Mappings
{
    public WorkflowDefinitionMapping()
    {
        For<WorkflowDefinition>()
            .PartitionKey(x => x.TenantId)
            .Column(x => x.Versions, map => map.Ignore());
    }
}