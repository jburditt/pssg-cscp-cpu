using AutoMapper;
using Database.Model;

namespace Resources;

public class CurrencyRepository(DatabaseContext databaseContext, IMapper mapper) : ICurrencyRepository
{
    public FindCurrencyResult FirstOrDefault(FindCurrencyQuery currencyQuery)
    {
        var query = databaseContext.TransactionCurrencySet;

        if (currencyQuery.StateCode != null)
        {
            query = query.Where(c => c.StateCode == (TransactionCurrency_StateCode)currencyQuery.StateCode);
        }

        if (currencyQuery.IsoCurrencyCode != null)
        {
            query = query.Where(p => p.IsoCurrencyCode == currencyQuery.IsoCurrencyCode);
        }

        var queryResults = query.FirstOrDefault();
        var currency = mapper.Map<Currency>(queryResults);
        return new FindCurrencyResult(currency);
    }

    public CurrencyResult Query()
    {
        var dynamicsCurrencies = databaseContext.TransactionCurrencySet
            // TODO move this to Command
            // TODO should be using enum outside of Contract
            .Where(c => c.StateCode == TransactionCurrency_StateCode.Active)
            // TODO move this to Command and use enum instead
            .Where(c => c.IsoCurrencyCode == "CAD")
            .ToList();
        var currencies = mapper.Map<IEnumerable<Currency>>(dynamicsCurrencies);
        return new CurrencyResult(currencies);
    }
}
