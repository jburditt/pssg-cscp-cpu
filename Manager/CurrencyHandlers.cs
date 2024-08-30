using Manager.Contract;
using Resources;

namespace Manager;

public class CurrencyHandlers(ICurrencyRepository currencyRepository) : 
    IRequestHandler<CurrencyQuery, CurrencyResult>,
    IRequestHandler<FindCurrencyQuery, FindCurrencyResult>
{
    public async Task<FindCurrencyResult> Handle(FindCurrencyQuery currencyQuery, CancellationToken cancellationToken = default)
    {
        var currencyResults = currencyRepository.FirstOrDefault(currencyQuery);
        return await Task.FromResult(new FindCurrencyResult(currencyResults.Currency));
    }

    public async Task<CurrencyResult> Handle(CurrencyQuery currencyQuery, CancellationToken cancellationToken = default)
    {
        var currencyResults = currencyRepository.Query(currencyQuery);
        return await Task.FromResult(new CurrencyResult(currencyResults.Currencies));
    }
}
