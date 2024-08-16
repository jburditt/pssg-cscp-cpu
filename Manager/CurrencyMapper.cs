using AutoMapper;
using Resources;

namespace Manager;

public class CurrencyMapper : Profile
{
    public CurrencyMapper()
    {
        CreateMap<Currency, Contract.Currency>();
    }
}
