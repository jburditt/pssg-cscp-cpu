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
            .ForMember(dest => dest.Approved, opts => opts.MapFrom(src => src.Vsd_LineItemApproved));

        CreateMap<InvoiceLineDetail, Vsd_InvoiceLineDetail>()
            .ForMember(dest => dest.Vsd_LineItemApproved, opts => opts.MapFrom(src => src.Approved))
            .ForMember(dest => dest.Vsd_InvoiceId, opts => opts.MapFrom(src => src.InvoiceId != null ? new EntityReference("vsd_invoice", src.InvoiceId.Value) : null))
            .ForMember(dest => dest.OwnerId, opts => opts.MapFrom(src => src.OwnerId != null ? new EntityReference("systemuser", src.OwnerId.Value) : null))
            .ForMember(dest => dest.Vsd_ProvinceStateId, opts => opts.MapFrom(src => new EntityReference("vsd_province", src.ProvinceStateId)));
    }
}
