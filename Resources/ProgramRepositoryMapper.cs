using AutoMapper;
using Database.Model;

namespace Resources;

public class ProgramRepositoryMapper : Profile
{
    public ProgramRepositoryMapper()
    {
        CreateMap<VSd_Program, Program>();

        CreateMap<StateCode, VSd_Program_StateCode>();
    }
}
