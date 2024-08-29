using Manager;
using Microsoft.Extensions.DependencyInjection;
using Resources;

namespace Gov.Cscp.Victims.Public;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddTransient<ContractHandlers>();
        services.AddTransient<IContractRepository, ContractRepository>();
        services.AddTransient<CurrencyHandlers>();
        services.AddTransient<ICurrencyRepository, CurrencyRepository>();
        services.AddTransient<ProgramHandlers>();
        services.AddTransient<IProgramRepository, ProgramRepository>();
        services.AddTransient<InvoiceHandlers>();
        services.AddTransient<IInvoiceRepository, InvoiceRepository>();
        services.AddTransient<InvoiceLineDetailHandlers>();
        services.AddTransient<IInvoiceLineDetailRepository, InvoiceLineDetailRepository>();
        services.AddTransient<PaymentHandlers>();
        services.AddTransient<IPaymentRepository, PaymentRepository>();
        return services;
    }
}
