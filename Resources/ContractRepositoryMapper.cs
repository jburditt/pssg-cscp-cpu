using AutoMapper;
using Database.Model;
using Manager.Contract;

namespace Resources;

public class ContractRepositoryMapper : Profile
{
    public ContractRepositoryMapper()
    {
        CreateMap<Vsd_Contract, Contract>();
    }
}
