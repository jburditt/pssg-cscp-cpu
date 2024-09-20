namespace Resources;

public interface IScheduleGRepository : IBaseRepository<ScheduleG>, IQueryRepository<ScheduleGQuery, ScheduleGResult>
{
    ScheduleGResult Query(ScheduleGQuery query);
}