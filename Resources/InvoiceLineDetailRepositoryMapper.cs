using AutoMapper;
using Database.Model;
using Manager.Contract;
using Microsoft.Xrm.Sdk;

namespace Resources;

public class InvoiceLineDetailRepositoryMapper : Profile
{
    public InvoiceLineDetailRepositoryMapper()
    {

        CreateMap<Vsd_InvoiceLineDetail, InvoiceLineDetail>()
            .ForMember(dest => dest.Approved, opts => opts.MapFrom(src => src.Vsd_LineItemApproved))
            .ForMember(dest => dest.AmountSimple, opts => opts.MapFrom(src => src.Vsd_AmountSimple))
            .ForMember(dest => dest.InvoiceId, opts => opts.MapFrom(src => src.Vsd_InvoiceId))
            .ForMember(dest => dest.InvoiceType, opts => opts.MapFrom(src => src.Vsd_InvoiceType))
            .ForMember(dest => dest.OwnerId, opts => opts.MapFrom(src => src.OwnerId))
            .ForMember(dest => dest.ProgramUnit, opts => opts.MapFrom(src => src.Vsd_ProgramUnit))
            .ForMember(dest => dest.ProvinceStateId, opts => opts.MapFrom(src => src.Vsd_ProvinceStateId))
            .ForMember(dest => dest.TaxExemption, opts => opts.MapFrom(src => src.Vsd_TaxExemption));

        CreateMap<InvoiceLineDetail, Vsd_InvoiceLineDetail>()
            .ForMember(dest => dest.Vsd_LineItemApproved, opts => opts.MapFrom(src => src.Approved))
            .ForMember(dest => dest.Vsd_AmountSimple, opts => opts.MapFrom(src => src.AmountSimple))
            .ForMember(dest => dest.Vsd_InvoiceId, opts => opts.MapFrom(src => src.InvoiceId != null ? new EntityReference("vsd_invoice", src.InvoiceId.Value) : null))
            .ForMember(dest => dest.Vsd_InvoiceType, opts => opts.MapFrom(src => src.InvoiceType))
            .ForMember(dest => dest.OwnerId, opts => opts.MapFrom(src => src.OwnerId != null ? new EntityReference("systemuser", src.OwnerId.Value) : null))
            .ForMember(dest => dest.Vsd_ProgramUnit, opts => opts.MapFrom(src => src.ProgramUnit))
            .ForMember(dest => dest.Vsd_ProvinceStateId, opts => opts.MapFrom(src => new EntityReference("vsd_province", src.ProvinceStateId)))
            .ForMember(dest => dest.Vsd_TaxExemption, opts => opts.MapFrom(src => src.TaxExemption));
    }
}
