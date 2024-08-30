using AutoMapper;
using Database.Model;

namespace Resources;

public class CurrencyRepository(DatabaseContext databaseContext, IMapper mapper) : ICurrencyRepository
{
    public FindCurrencyResult FirstOrDefault(FindCurrencyQuery currencyQuery)
    {
        var queryResults = databaseContext.TransactionCurrencySet
            .WhereIf(currencyQuery.StateCode != null, c => c.StateCode == (TransactionCurrency_StateCode?)currencyQuery.StateCode)
            .WhereIf(currencyQuery.IsoCurrencyCode == null, p => p.IsoCurrencyCode == currencyQuery.IsoCurrencyCode)
            .FirstOrDefault();
        var currency = mapper.Map<Currency>(queryResults);
        return new FindCurrencyResult(currency);
    }

    public CurrencyResult Query(CurrencyQuery currencyQuery)
    {
        var queryResults = databaseContext.TransactionCurrencySet
            .WhereIf(currencyQuery.StateCode == null, c => c.StateCode == (TransactionCurrency_StateCode?)currencyQuery.StateCode)
            .WhereIf(currencyQuery.IsoCurrencyCode == null, c => c.IsoCurrencyCode == currencyQuery.IsoCurrencyCode)
            .ToList();
        var currencies = mapper.Map<IEnumerable<Currency>>(queryResults);
        return new CurrencyResult(currencies);
    }
}
