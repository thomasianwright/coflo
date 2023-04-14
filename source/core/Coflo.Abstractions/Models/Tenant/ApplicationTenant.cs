using Coflo.Abstractions.Contracts.Tenant;

namespace Coflo.Abstractions.Models.Tenant;

public class ApplicationTenant : IApplicationTenant
{
    public Guid TenantId { get; set; }
    public string Name { get; set; }
}