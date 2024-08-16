using AutoMapper;
using Database.Model;

namespace Resources;

public class ProgramRepositoryMapper : Profile
{
    public ProgramRepositoryMapper()
    {
        CreateMap<Vsd_Program, Program>();

        CreateMap<StateCode, Vsd_Program_StateCode>();
    }
}
