namespace Resources;

public class ContractRepositoryMapper : Profile
{
    public ContractRepositoryMapper()
    {
        CreateMap<Vsd_Contract, Contract>()
            .ForMember(dest => dest.ContractType, opts => opts.MapFrom(src => src.Vsd_Type))
            .ForMember(dest => dest.ClonedContractId, opts => opts.MapFrom(src => src.Vsd_ClonedContractId.Id))
            .ForMember(dest => dest.CustomerId, opts => opts.MapFrom(src => src.Vsd_Customer.Id));

        CreateMap<Contract, Vsd_Contract>()
            .ForMember(dest => dest.Vsd_Type, opts => opts.MapFrom(src => src.ContractType))
            .ForMember(dest => dest.Vsd_ClonedContractId, opts => opts.MapFrom(src => src.ClonedContractId != null ? new EntityReference(Vsd_Contract.EntityLogicalName, src.ClonedContractId.Value) : null))
            .ForMember(dest => dest.Vsd_Customer, opts => opts.MapFrom(src => new EntityReference(Account.EntityLogicalName, src.CustomerId)));
    }
}
