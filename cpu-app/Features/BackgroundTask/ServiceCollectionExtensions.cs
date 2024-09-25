using Microsoft.Extensions.DependencyInjection;

namespace Gov.Cscp.Victims.Public.Background;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBackgroundTask(this IServiceCollection services)
    {
        services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
        services.AddHostedService<QueuedHostedService>();
        return services;
    }
}
