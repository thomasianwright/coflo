namespace Coflo.Abstractions.Contracts.Tenant;

public interface ITenantScope
{
    public Guid? TenantId { get; set; }
}