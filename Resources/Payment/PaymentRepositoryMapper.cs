namespace Resources;

public class PaymentRepositoryMapper : Profile
{
    public PaymentRepositoryMapper()
    {
        CreateMap<Database.Model.Vsd_Payment, Payment>()
            // NOTE in theory, this shouldn't be necessary
            .ForMember(dest => dest.PaymentTotal, opt => opt.MapFrom(src => src.Vsd_PaymentTotal));
    }
}
