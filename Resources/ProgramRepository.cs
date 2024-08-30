using AutoMapper;
using Database.Model;
using Manager.Contract;

namespace Resources;

public class ProgramRepository(DatabaseContext databaseContext, IMapper mapper) : IProgramRepository
{
    public ProgramResult Query(ProgramQuery programQuery)
    {
        var queryResults = databaseContext.Vsd_ProgramSet
            .WhereIf(programQuery.StateCode != null, c => c.StateCode == mapper.Map<Vsd_Program_StateCode>(programQuery.StateCode))
            .ToList();
        var programs = mapper.Map<IEnumerable<Program>>(queryResults);
        return new ProgramResult(programs);
    }

    public ProgramResult GetApproved()
    {
        var queryResults = (
            from p in databaseContext.Vsd_ProgramSet
            join c in databaseContext.Vsd_ContractSet on p.Vsd_ContractId.Id equals c.Vsd_ContractId
            where p.StateCode == Vsd_Program_StateCode.Active
            where p.StatusCode != Vsd_Program_StatusCode.Draft && p.StatusCode != Vsd_Program_StatusCode.ApplicationInfoSent && p.StatusCode != Vsd_Program_StatusCode.ApplicationInfoReceived
            where c.StatusCode == Vsd_Contract_StatusCode.DulyExecuted
            where c.Vsd_Type != Vsd_ContractType.TuaCommunityAccountabilityPrograms
            select new { Program = p, Contract = c })
                .ToList()
                .Select(pc => pc.Program);
        var programs = mapper.Map<IEnumerable<Program>>(queryResults);
        return new ProgramResult(programs);
    }
}
