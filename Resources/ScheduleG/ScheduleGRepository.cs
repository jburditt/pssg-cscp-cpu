namespace Resources;

public class ScheduleGRepository : BaseRepository<Vsd_ScheduleG, ScheduleG>, IScheduleGRepository
{
    public ScheduleGRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper) { }

    public ScheduleGResult Query(ScheduleGQuery query)
    {
        var queryResults = _databaseContext.Vsd_ScheduleGSet
            .WhereIf(query.Id != null, x => x.Id == query.Id)
            .WhereIf(query.ProgramId != null, x => x.Vsd_Program.Id == query.ProgramId)
            .WhereIf(query.Quarter != null, x => x.Vsd_Cpu_ReportingPeriod == (Vsd_ScheduleG_Vsd_Cpu_ReportingPeriod?)query.Quarter)
            .ToList();

        var scheduleGs = _mapper.Map<IEnumerable<ScheduleG>>(queryResults);
        return new ScheduleGResult(scheduleGs);
    }
}