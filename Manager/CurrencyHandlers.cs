namespace Manager;

public class CurrencyHandlers(ICurrencyRepository repository) : FindQueryBaseHandlers<ICurrencyRepository, Currency, FindCurrencyQuery, FindCurrencyResult, CurrencyQuery, CurrencyResult>(repository),
    IRequestHandler<CurrencyQuery, CurrencyResult>,
    IRequestHandler<FindCurrencyQuery, FindCurrencyResult>
{

}
