namespace Resources;

public class PaymentRepositoryMapper : Profile
{
    public PaymentRepositoryMapper()
    {
        CreateMap<Vsd_Payment, Payment>()
            // NOTE in theory, this shouldn't be necessary, global mapper and prefix rules should automate this, but without this line the mapping fails
            .ForMember(dest => dest.PaymentTotal, opt => opt.MapFrom(src => src.Vsd_PaymentTotal.Value));
    }
}
