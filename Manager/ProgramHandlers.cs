using AutoMapper;
using Manager.Contract;
using Resources;

namespace Manager;

public class ProgramHandlers(IProgramRepository programRepository, IMapper mapper) : 
    IRequestHandler<ProgramQuery, ProgramResult>,
    IRequestHandler<ProgramResultEmptyMessage, ProgramResult>
{
    // TODO remove default cancellation token
    public async Task<ProgramResult> Handle(ProgramQuery programQuery, CancellationToken cancellationToken = default)
    {
        //var resourcesProgramQuery = mapper.Map<Resources.ProgramQuery>(programQuery);
        var resourcesProgramQuery = new ProgramQuery() { StateCode = (StateCode)(int)(programQuery.StateCode ?? 0) };
        var programResults = programRepository.Query(resourcesProgramQuery);
        var programs = mapper.Map<IEnumerable<Program>>(programResults.Programs);
        return await Task.FromResult(new ProgramResult(programs));
    }

    // TODO replace empty with GetApprovedCommand
    public async Task<ProgramResult> Handle(ProgramResultEmptyMessage dummy, CancellationToken cancellationToken = default)
    {
        var programResults = programRepository.GetApproved();
        var programs = mapper.Map<IEnumerable<Program>>(programResults.Programs);
        return await Task.FromResult(new ProgramResult(programs));
    }
}
