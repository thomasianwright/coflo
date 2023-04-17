using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;

namespace Coflo.Hosting.Orchestrator;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOrchestrator(this IServiceCollection services, Action<CapOptions> capOptions)
    {
        services.AddMediator();
        services.AddCap(capOptions);
        
        
        return services;
    }
}