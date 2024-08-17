using AutoMapper;
using Manager.Contract;
using Resources;

namespace Manager;

public class CurrencyHandlers(ICurrencyRepository currentRepository, IMapper mapper) : IRequestHandler<CurrencyQuery, Contract.CurrencyResult>
{
    public async Task<Contract.CurrencyResult> Handle(CurrencyQuery currencyQuery, CancellationToken cancellationToken = default)
    {
        var currencies = currentRepository.Query();
        var currencyResults = mapper.Map<IEnumerable<Contract.Currency>>(currencies.Currencies);
        return new Contract.CurrencyResult(currencyResults);
    }
}
