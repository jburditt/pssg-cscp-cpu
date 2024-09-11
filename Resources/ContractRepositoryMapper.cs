using AutoMapper;
using Database.Model;
using Manager.Contract;

namespace Resources;

public class ContractRepositoryMapper : Profile
{
    public ContractRepositoryMapper()
    {
        CreateMap<Vsd_Contract, Contract>();

        CreateMap<Contract, Vsd_Contract>()
            .ForMember(dest => dest.Vsd_Type, opts => opts.MapFrom(src => src.ContractType));
    }
}
