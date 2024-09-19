namespace Resources;

public interface ICurrencyRepository
{
    FindCurrencyResult FirstOrDefault(FindCurrencyQuery currencyQuery);
    CurrencyResult Query(CurrencyQuery currencyQuery);
}
