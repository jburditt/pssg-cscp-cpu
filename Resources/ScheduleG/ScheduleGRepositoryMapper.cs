namespace Resources;

public class ScheduleGRepositoryMapper : Profile
{
    public ScheduleGRepositoryMapper()
    {
        CreateMap<Vsd_ScheduleG, ScheduleG>()
            .ForMember(dest => dest.Quarter, opts => opts.MapFrom(src => src.Vsd_Cpu_ReportingPeriod))
            .ForMember(dest => dest.ServiceProviderId, opts => opts.MapFrom(src => src.Vsd_ServiceProvider.Id))
            .ForMember(dest => dest.ProgramId, opts => opts.MapFrom(src => src.Vsd_Program.Id))
            .ForMember(dest => dest.ContractId, opts => opts.MapFrom(src => src.Vsd_Contract.Id));

        CreateMap<ScheduleG, Vsd_ScheduleG>()
            .ForMember(dest => dest.Vsd_Cpu_ReportingPeriod, opts => opts.MapFrom(src => src.Quarter))
            .ForMember(dest => dest.Vsd_ServiceProvider, opts => opts.MapFrom(src => src.ServiceProviderId != null ? new EntityReference(Account.EntityLogicalName, src.ServiceProviderId.Value) : null))
            .ForMember(dest => dest.Vsd_Program, opts => opts.MapFrom(src => new EntityReference(Vsd_Program.EntityLogicalName, src.ProgramId)))
            .ForMember(dest => dest.Vsd_Contract, opts => opts.MapFrom(src => src.ContractId != null ? new EntityReference(Vsd_Contract.EntityLogicalName, src.ContractId.Value) : null));
    }
}
