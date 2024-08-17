using AutoMapper;
using Database.Model;
using Manager.Contract;

namespace Resources;

public class CurrencyRepository(DatabaseContext databaseContext, IMapper mapper) : ICurrencyRepository
{
    public CurrencyResult Query()
    {
        var dynamicsCurrencies = databaseContext.TransactionCurrencySet
            // TODO move this to Command
            .Where(c => c.StateCode == TransactionCurrency_StateCode.Active)
            // TODO move this to Command and use enum instead
            .Where(c => c.IsoCurrencyCode == "CAD")
            .ToList();
        var currencies = mapper.Map<IEnumerable<Currency>>(dynamicsCurrencies);
        return new CurrencyResult(currencies);
    }
}
