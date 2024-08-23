using AutoMapper;
using Database.Model;
using Manager.Contract;

namespace Resources;

public class ProgramRepositoryMapper : Profile
{
    public ProgramRepositoryMapper()
    {
        CreateMap<Vsd_Program, Program>()
            .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.Vsd_ContractId.Id))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId.Id))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Vsd_Name))
            .ForMember(dest => dest.ContractName, opts => opts.MapFrom(src => src.Vsd_ContractIdName));

        CreateMap<StateCode, Vsd_Program_StateCode>();
    }
}
