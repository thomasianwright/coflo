using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;

namespace Coflo.Hosting.Worker;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWorker(this IServiceCollection services, Action<CapOptions> capOptions)
    {
        services.AddMediator();
        services.AddCap(capOptions);
        
        return services;
    }
}