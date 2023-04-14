namespace Coflo.Abstractions.Contracts.Tenant;

public interface IApplicationTenant
{
    public Guid TenantId { get; set; }
    public string Name { get; set; }
}