namespace Resources;

public interface IScheduleGRepository : IBaseRepository<ScheduleG>
{
    ScheduleGResult Query(ScheduleGQuery query);
}