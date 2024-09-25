namespace Resources;

public class CurrencyRepositoryMapper : Profile
{
    public CurrencyRepositoryMapper()
    {
        CreateMap<TransactionCurrency, Currency>();
    }
}
