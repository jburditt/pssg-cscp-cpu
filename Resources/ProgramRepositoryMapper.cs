using AutoMapper;
using Database.Model;
using Manager.Contract;

namespace Resources;

public class ProgramRepositoryMapper : Profile
{
    public ProgramRepositoryMapper()
    {
        CreateMap<Vsd_Program, Program>()
            .ForMember(dest => dest.ContractName, opts => opts.MapFrom(src => src.Vsd_ContractIdName));

        CreateMap<StateCode, Vsd_Program_StateCode>();
    }
}
