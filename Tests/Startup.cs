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

        services.AddTransient<IInvoiceLineDetailRepository, InvoiceLineDetailRepository>();
        services.AddTransient<IContractRepository, ContractRepository>();
        services.AddTransient<IInvoiceRepository, InvoiceRepository>();
        services.AddTransient<IPaymentRepository, PaymentRepository>();
        services.AddTransient<IProgramRepository, ProgramRepository>();
        services.AddTransient<ICurrencyRepository, CurrencyRepository>();
        services.AddTransient<IScheduleGRepository, ScheduleGRepository>();

        // add dynamics database adapter
        services.AddDatabase(configuration);
        services.AddTransient<SeedDatabase>();
    }
}
