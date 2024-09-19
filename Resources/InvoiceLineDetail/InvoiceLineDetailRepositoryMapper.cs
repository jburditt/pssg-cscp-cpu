namespace Resources;

public class InvoiceLineDetailRepositoryMapper : Profile
{
    public InvoiceLineDetailRepositoryMapper()
    {
        CreateMap<Vsd_InvoiceLineDetail, InvoiceLineDetail>()
            .ForMember(dest => dest.Approved, opts => opts.MapFrom(src => src.Vsd_LineItemApproved))
            // NOTE in theory, the below shouldn't be necessary, since we have automapper rules for EntityReference to Guid? and removing the prefix "Vsd_"
            .ForMember(dest => dest.InvoiceType, opts => opts.MapFrom(src => src.Vsd_InvoiceType))
            .ForMember(dest => dest.AmountSimple, opts => opts.MapFrom(src => src.Vsd_AmountSimple))
            .ForMember(dest => dest.ProgramUnit, opts => opts.MapFrom(src => src.Vsd_ProgramUnit))
            .ForMember(dest => dest.TaxExemption, opts => opts.MapFrom(src => src.Vsd_TaxExemption))
            .ForMember(dest => dest.InvoiceId, opts => opts.MapFrom(src => src.Vsd_InvoiceId.Id))
            .ForMember(dest => dest.OwnerId, opts => opts.MapFrom(src => src.OwnerId.Id))
            .ForMember(dest => dest.ProvinceStateId, opts => opts.MapFrom(src => src.Vsd_ProvinceStateId.Id));

        CreateMap<InvoiceLineDetail, Vsd_InvoiceLineDetail>()
            .ForMember(dest => dest.Vsd_LineItemApproved, opts => opts.MapFrom(src => src.Approved))
            // NOTE in theory, the below shouldn't be necessary, the rule for prefix "Vsd_" has been added and tested to make sure it is added before the other rules
            .ForMember(dest => dest.Vsd_InvoiceType, opts => opts.MapFrom(src => src.InvoiceType))
            .ForMember(dest => dest.Vsd_AmountSimple, opts => opts.MapFrom(src => src.AmountSimple))
            .ForMember(dest => dest.Vsd_ProgramUnit, opts => opts.MapFrom(src => src.ProgramUnit))
            .ForMember(dest => dest.Vsd_TaxExemption, opts => opts.MapFrom(src => src.TaxExemption))
            // TODO replace "systemuser" and "vsd_province" with constants e.g. SystemUser.EntityLogicalName
            .ForMember(dest => dest.Vsd_InvoiceId, opts => opts.MapFrom(src => src.InvoiceId != null ? new EntityReference(Vsd_Invoice.EntityLogicalName, src.InvoiceId.Value) : null))
            .ForMember(dest => dest.OwnerId, opts => opts.MapFrom(src => src.OwnerId != null ? new EntityReference("systemuser", src.OwnerId.Value) : null))
            .ForMember(dest => dest.Vsd_ProvinceStateId, opts => opts.MapFrom(src => new EntityReference("vsd_province", src.ProvinceStateId)));
    }
}
