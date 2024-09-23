namespace Manager;

public class ScheduleGHandlers(IScheduleGRepository repository) :
    QueryBaseHandlers<IScheduleGRepository, ScheduleG, ScheduleGQuery, ScheduleGResult>(repository),
    IRequestHandler<ScheduleGQuery, ScheduleGResult>,
    IRequestHandler<InsertCommand<ScheduleG>, Guid>
{

}
