using AutoMapper;
using Manager.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager;

public class CurrencyMapper : Profile
{
    public CurrencyMapper()
    {
        CreateMap<Database.Model.TransactionCurrency, Currency>();
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TransactionCurrencyId))
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CurrencyName))
            //.ForMember(dest => dest.IsoCurrencyCode, opt => opt.MapFrom(src => src.IsoCurrencyCode))
            //.ForMember(dest => dest.CurrencySymbol, opt => opt.MapFrom(src => src.CurrencySymbol));
    }
}
