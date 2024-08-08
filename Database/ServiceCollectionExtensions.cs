using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace Database
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IOrganizationServiceAsync>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<ServiceClient>>();
                var connectionString = configuration["DYNAMICS_CONNECTIONSTRING"];
                var client = new ServiceClient(connectionString, logger);
                if (!client.IsReady) throw new InvalidOperationException($"Failed to connect to Dataverse: {client.LastError}", client.LastException);
                return client;
            });
            services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<IOrganizationServiceAsync>();
                return new DatabaseContext(client);
            });

            return services;
        }
    }
}
