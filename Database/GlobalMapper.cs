using AutoMapper;

namespace Database;

public class GlobalMapper : Profile
{
    public GlobalMapper()
    {
        CreateMap<Microsoft.Xrm.Sdk.Money, decimal>()
            .ConvertUsing(x => x.Value);
    }
}
