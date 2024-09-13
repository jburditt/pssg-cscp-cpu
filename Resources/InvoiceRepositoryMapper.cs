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
            .ForMember(dest => dest.Vsd_Cpu_InvoiceType, opts => opts.MapFrom(src => src.CpuInvoiceType))
            .ForMember(dest => dest.Vsd_Cpu_ScheduledPaymentDate, opts => opts.MapFrom(src => src.CpuScheduledPaymentDate))
            .ForMember(dest => dest.Vsd_Cvap_InvoiceType, opts => opts.MapFrom(src => src.CvapInvoiceType))
            .ForMember(dest => dest.Vsd_InvoicedAte, opts => opts.MapFrom(src => src.InvoiceDate))
            .ForMember(dest => dest.Vsd_ContractId, opts => opts.MapFrom(src => src.ContractId != null ? new EntityReference(Vsd_Contract.EntityLogicalName, src.ContractId.Value) : null))
            .ForMember(dest => dest.OwnerId, opts => opts.MapFrom(src => src.OwnerId != null ? new EntityReference("systemuser", src.OwnerId.Value) : null))
            .ForMember(dest => dest.Vsd_ProgramId, opts => opts.MapFrom(src => src.ProgramId != null ? new EntityReference(Vsd_Program.EntityLogicalName, src.ProgramId.Value) : null))
            .ForMember(dest => dest.Vsd_ProvinceStateId, opts => opts.MapFrom(src => src.ProvinceStateId != null ? new EntityReference("vsd_province", src.ProvinceStateId.Value) : null))
            .ForMember(dest => dest.TransactionCurrencyId, opts => opts.MapFrom(src => src.CurrencyId != null ? new EntityReference(TransactionCurrency.EntityLogicalName, src.CurrencyId.Value) : null));
    }
}
