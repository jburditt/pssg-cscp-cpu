using AutoMapper;
using Database.Model;
using Manager.Contract;

namespace Resources;

public class ProgramRepository : BaseRepository, IProgramRepository
{
    private readonly IMapper _mapper;

    public ProgramRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext)
    {
        this._mapper = mapper;
    }

    public Guid Upsert(Program program)
    {
        var entity = _mapper.Map<Vsd_Program>(program);
        entity.Id = base.Upsert(entity);
        return entity.Id;
    }

    public ProgramResult Query(ProgramQuery programQuery)
    {
        var queryResults = _databaseContext.Vsd_ProgramSet
            .WhereIf(programQuery.Id != null, x => x.Id == programQuery.Id)
            .WhereIf(programQuery.StateCode != null, c => c.StateCode == _mapper.Map<Vsd_Program_StateCode>(programQuery.StateCode))
            .ToList();
        var programs = _mapper.Map<IEnumerable<Program>>(queryResults);
        return new ProgramResult(programs);
    }

    // safe delete, use try delete for faster deletes
    public bool Delete(Guid id)
    {
        var entity = _databaseContext.Vsd_ProgramSet.FirstOrDefault(x => x.Vsd_ProgramId == id);
        if (entity == null)
        {
            return false;
        }
        return base.Delete(entity);
    }

    public ProgramResult GetApproved()
    {
        var queryResults = (
            from p in _databaseContext.Vsd_ProgramSet
            join c in _databaseContext.Vsd_ContractSet on p.Vsd_ContractId.Id equals c.Vsd_ContractId
            where p.StateCode == Vsd_Program_StateCode.Active
            where p.StatusCode != Vsd_Program_StatusCode.Draft && p.StatusCode != Vsd_Program_StatusCode.ApplicationInfoSent && p.StatusCode != Vsd_Program_StatusCode.ApplicationInfoReceived
            where c.StatusCode == Vsd_Contract_StatusCode.DulyExecuted
            where c.Vsd_Type != Vsd_ContractType.TuaCommunityAccountabilityPrograms
            select p)
                .ToList();
        var programs = _mapper.Map<IEnumerable<Program>>(queryResults);
        return new ProgramResult(programs);
    }
}
