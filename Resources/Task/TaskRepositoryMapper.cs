using TaskDto = Manager.Contract.Task;
using TaskEntity = Database.Model.Task;

namespace Resources;

public class TaskRepositoryMapper : Profile
{
    public TaskRepositoryMapper()
    {
        CreateMap<TaskEntity, TaskDto>()
            .ForMember(dest => dest.TaskTypeId, opts => opts.MapFrom(src => src.Vsd_TaskTypeId.Id))
            .ForMember(dest => dest.RegardingObjectId, opts => opts.MapFrom(src => src.RegardingObjectId.Id))
            .ForMember(dest => dest.ProgramId, opts => opts.MapFrom(src => src.Vsd_ProgramId.Id))
            .ForMember(dest => dest.ScheduleGId, opts => opts.MapFrom(src => src.Vsd_ScheduleGId.Id));

        CreateMap<TaskDto, TaskEntity>()
            .ForMember(dest => dest.Vsd_TaskTypeId, opts => opts.MapFrom(src => src.TaskTypeId != null ? new EntityReference("vsd_tasktype", src.TaskTypeId.Value) : null))
            .ForMember(dest => dest.RegardingObjectId, opts => opts.MapFrom(src => new EntityReference(Vsd_Contract.EntityLogicalName, src.RegardingObjectId)))
            .ForMember(dest => dest.Vsd_ProgramId, opts => opts.MapFrom(src => src.ProgramId != null ? new EntityReference(Vsd_Program.EntityLogicalName, src.ProgramId.Value) : null))
            .ForMember(dest => dest.Vsd_ScheduleGId, opts => opts.MapFrom(src => src.ScheduleGId != null ? new EntityReference(Vsd_ScheduleG.EntityLogicalName, src.ScheduleGId.Value) : null));
    }
}
