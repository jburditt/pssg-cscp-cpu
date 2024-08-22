using AutoMapper;
using Manager.Contract;

namespace Resources;

public class PaymentRepositoryMapper : Profile
{
    public PaymentRepositoryMapper()
    {
        CreateMap<Database.Model.Vsd_Payment, Payment>()
            .ForMember(dest => dest.PaymentTotal, opt => opt.MapFrom(src => src.Vsd_PaymentTotal));
    }
}
