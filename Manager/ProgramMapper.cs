using AutoMapper;
using Resources;

namespace Manager;

public class ProgramMapper : Profile
{
    public ProgramMapper()
    {
        CreateMap<Program, Contract.Program>();
    }
}
