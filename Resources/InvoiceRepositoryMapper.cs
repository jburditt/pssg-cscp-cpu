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
            .ForMember(dest => dest.ContractId, opts => opts.MapFrom(src => src.Vsd_ContractId.Id))
            .ForMember(dest => dest.CpuInvoiceType, opts => opts.MapFrom(src => src.Vsd_Cpu_InvoiceType))
            .ForMember(dest => dest.CpuScheduledPaymentDate, opts => opts.MapFrom(src => src.Vsd_Cpu_ScheduledPaymentDate))
            .ForMember(dest => dest.CurrencyId, opts => opts.MapFrom(src => src.TransactionCurrencyId.Id))
            .ForMember(dest => dest.CvapInvoiceType, opts => opts.MapFrom(src => src.Vsd_Cvap_InvoiceType))
            .ForMember(dest => dest.InvoiceDate, opts => opts.MapFrom(src => src.Vsd_InvoicedAte))
            .ForMember(dest => dest.MethodOfPayment, opts => opts.MapFrom(src => src.Vsd_MethodOfPayment))
            .ForMember(dest => dest.Origin, opts => opts.MapFrom(src => src.Vsd_Origin))
            .ForMember(dest => dest.OwnerId, opts => opts.MapFrom(src => src.OwnerId.Id))
            .ForMember(dest => dest.PayeeId, opts => opts.MapFrom(src => src.Vsd_Payee.Id))
            .ForMember(dest => dest.PaymentAdviceComments, opts => opts.MapFrom(src => src.Vsd_PaymentAdviceComments))
            .ForMember(dest => dest.ProgramId, opts => opts.MapFrom(src => src.Vsd_ProgramId.Id))
            .ForMember(dest => dest.ProgramUnit, opts => opts.MapFrom(src => src.Vsd_ProgramUnit))
            .ForMember(dest => dest.ProvinceStateId, opts => opts.MapFrom(src => src.Vsd_ProvinceStateId.Id))
            .ForMember(dest => dest.TaxExemption, opts => opts.MapFrom(src => src.Vsd_TaxExemption));

        CreateMap<Invoice, Vsd_Invoice>()
            .ForMember(dest => dest.Vsd_ContractId, opts => opts.MapFrom(src => src.ContractId != null ? new EntityReference("vsd_contract", src.ContractId.Value) : null))
            .ForMember(dest => dest.OwnerId, opts => opts.MapFrom(src => src.OwnerId != null ? new EntityReference("systemuser", src.OwnerId.Value) : null))
            .ForMember(dest => dest.Vsd_ProgramId, opts => opts.MapFrom(src => src.ProgramId != null ? new EntityReference("vsd_program", src.ProgramId.Value) : null))
            .ForMember(dest => dest.Vsd_ProvinceStateId, opts => opts.MapFrom(src => src.ProvinceStateId != null ? new EntityReference("vsd_province", src.ProvinceStateId.Value) : null));
    }
}
