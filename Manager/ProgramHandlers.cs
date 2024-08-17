using AutoMapper;
using Manager.Contract;
using Resources;

namespace Manager;



public class ProgramHandlers(IProgramRepository programRepository, IMapper mapper) : 
    IRequestHandler<Contract.ProgramQuery, Contract.ProgramResult>,
    IRequestHandler<ProgramResultEmptyMessage, ProgramResult>
{
    // TODO remove default cancellation token
    public async Task<Contract.ProgramResult> Handle(Contract.ProgramQuery programQuery, CancellationToken cancellationToken = default)
    {
        //var resourcesProgramQuery = mapper.Map<Resources.ProgramQuery>(programQuery);
        var resourcesProgramQuery = new ProgramQuery() { StateCode = (StateCode)(int)(programQuery.StateCode ?? 0) };
        var programResults = programRepository.Query(resourcesProgramQuery);
        var programs = mapper.Map<IEnumerable<Contract.Program>>(programResults.Programs);
        return new Contract.ProgramResult(programs);
    }

    public async Task<ProgramResult> Handle(ProgramResultEmptyMessage dummy, CancellationToken cancellationToken = default)
    {
        var programResults = programRepository.GetApproved();
        var programs = mapper.Map<IEnumerable<Contract.Program>>(programResults.Programs);
        return new ProgramResult(programs);
    }
}
