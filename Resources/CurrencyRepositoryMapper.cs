using AutoMapper;

namespace Resources;

public class CurrencyRepositoryMapper : Profile
{
    public CurrencyRepositoryMapper()
    {
        CreateMap<Database.Model.TransactionCurrency, Currency>();
    }
}
