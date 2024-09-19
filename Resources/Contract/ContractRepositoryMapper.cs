namespace Resources;

public class ContractRepositoryMapper : Profile
{
    public ContractRepositoryMapper()
    {
        CreateMap<Vsd_Contract, Contract>()
            .ForMember(dest => dest.ClonedContractId, opts => opts.MapFrom(src => src.Vsd_ClonedContractId.Id))
            .ForMember(dest => dest.ContractType, opts => opts.MapFrom(src => src.Vsd_Type));

        CreateMap<Contract, Vsd_Contract>()
            .ForMember(dest => dest.Vsd_Type, opts => opts.MapFrom(src => src.ContractType))
            .ForMember(dest => dest.Vsd_ClonedContractId, opts => opts.MapFrom(src => src.ClonedContractId != null ? new EntityReference(Vsd_Contract.EntityLogicalName, src.ClonedContractId.Value) : null));
    }
}
