using AutoMapper;
using Microsoft.Xrm.Sdk;

namespace Database;

public class GlobalMapper : Profile
{
    public GlobalMapper()
    {
        RecognizeDestinationPrefixes("Vsd_");

        RecognizePrefixes("Vsd_");

        CreateMap<Money, decimal>()
            .ConvertUsing(src => src.Value);
        CreateMap<EntityReference, Guid>()
            .ConvertUsing(src => src.Id);
        CreateMap<EntityReference, Guid?>()
            .ConvertUsing(src => src.Id);
    }
}
