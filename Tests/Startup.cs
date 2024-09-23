public class Startup
{
    /// <summary>
    /// Register dependencies needed for xunit tests
    /// NOTE to register dependencies used by making calls from HttpClient, use CustomWebApplicationFactory
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<ApplicationVersionInfo>()
            .AddEnvironmentVariables()
            .Build();
        services.AddSingleton<IConfiguration>(configuration);

        services.AddAutoMapper();

        services.AddHandlers();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<HandlerAssemblyMarker>());

        // add dynamics database adapter
        services.AddDatabase(configuration);
        services.AddTransient<SeedDatabase>();
    }
}
