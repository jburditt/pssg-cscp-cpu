using AutoMapper;
using Database.Model;
using Manager.Contract;
using Microsoft.Xrm.Sdk;

namespace Resources;

public class InvoiceRepositoryMapper : Profile
{
    public InvoiceRepositoryMapper()
    {
        CreateMap<Vsd_Invoice, Invoice>()
            .ForMember(dest => dest.CpuInvoiceType, opts => opts.MapFrom(src => src.Vsd_Cpu_InvoiceType))
            .ForMember(dest => dest.CpuScheduledPaymentDate, opts => opts.MapFrom(src => src.Vsd_Cpu_ScheduledPaymentDate))
            .ForMember(dest => dest.CurrencyId, opts => opts.MapFrom(src => src.TransactionCurrencyId.Id))
            .ForMember(dest => dest.CvapInvoiceType, opts => opts.MapFrom(src => src.Vsd_Cvap_InvoiceType))
            .ForMember(dest => dest.InvoiceDate, opts => opts.MapFrom(src => src.Vsd_InvoicedAte));

        CreateMap<Invoice, Vsd_Invoice>()
            .ForMember(dest => dest.Vsd_ContractId, opts => opts.MapFrom(src => src.ContractId != null ? new EntityReference("vsd_contract", src.ContractId.Value) : null))
            .ForMember(dest => dest.OwnerId, opts => opts.MapFrom(src => src.OwnerId != null ? new EntityReference("systemuser", src.OwnerId.Value) : null))
            .ForMember(dest => dest.Vsd_ProgramId, opts => opts.MapFrom(src => src.ProgramId != null ? new EntityReference("vsd_program", src.ProgramId.Value) : null))
            .ForMember(dest => dest.Vsd_ProvinceStateId, opts => opts.MapFrom(src => src.ProvinceStateId != null ? new EntityReference("vsd_province", src.ProvinceStateId.Value) : null));
    }
}
