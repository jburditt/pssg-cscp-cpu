using AutoMapper;
using Manager.Contract;
using Resources;

namespace Manager;

public class CurrencyHandlers(ICurrencyRepository currencyRepository, IMapper mapper) : IRequestHandler<CurrencyQuery, CurrencyResult>
{
    public async Task<CurrencyResult> Handle(CurrencyQuery currencyQuery, CancellationToken cancellationToken = default)
    {
        var currencyResults = currencyRepository.Query();
        //var currencyResults = mapper.Map<IEnumerable<Currency>>(currencies.Currencies);
        return new CurrencyResult(currencyResults.Currencies);
    }
}
