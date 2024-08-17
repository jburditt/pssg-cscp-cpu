using AutoMapper;
using Manager.Contract;

namespace Resources;

public class CurrencyRepositoryMapper : Profile
{
    public CurrencyRepositoryMapper()
    {
        CreateMap<Database.Model.TransactionCurrency, Currency>();
    }
}
