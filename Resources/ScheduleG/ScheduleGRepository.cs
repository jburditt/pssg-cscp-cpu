namespace Resources;

public class ScheduleGRepository : BaseRepository, IScheduleGRepository
{
    private readonly IMapper _mapper;

    public ScheduleGRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext)
    {
        _mapper = mapper;
    }

    #region CRUD

    //public Guid Insert(ScheduleG dto)
    //{
    //    // TODO user Insert<T> from BaseRepository instead
    //    var entity = _mapper.Map<Vsd_ScheduleG>(dto);
    //    return base.Insert(entity);
    //}

    //public Guid Upsert(ScheduleG dto)
    //{
    //    var entity = _mapper.Map<Vsd_ScheduleG>(dto);
    //    return base.Upsert(entity);
    //}

    public ScheduleGResult Query(ScheduleGQuery query)
    {
        var queryResults = _databaseContext.Vsd_ScheduleGSet
            .WhereIf(query.ProgramId != null, x => x.Vsd_Program.Id == query.ProgramId)
            .WhereIf(query.Quarter != null, x => x.Vsd_Cpu_ReportingPeriod == (Vsd_ScheduleG_Vsd_Cpu_ReportingPeriod?)query.Quarter)
            .ToList();

        var scheduleGs = _mapper.Map<IEnumerable<ScheduleG>>(queryResults);
        return new ScheduleGResult(scheduleGs);
    }

    #endregion CRUD
}