using AutoMapper;
using Manager.Contract;
using Resources;

namespace Manager;

public class ProgramHandlers(IProgramRepository programRepository, IMapper mapper) : 
    IRequestHandler<ProgramQuery, ProgramResult>,
    IRequestHandler<GetApprovedCommand, ProgramResult>
{
    public async Task<ProgramResult> Handle(ProgramQuery programQuery, CancellationToken cancellationToken = default)
    {
        var programResults = programRepository.Query(programQuery);
        var programs = mapper.Map<IEnumerable<Program>>(programResults.Programs);
        return await Task.FromResult(new ProgramResult(programs));
    }

    public async Task<ProgramResult> Handle(GetApprovedCommand dummy, CancellationToken cancellationToken = default)
    {
        var programResults = programRepository.GetApproved();
        var programs = mapper.Map<IEnumerable<Program>>(programResults.Programs);
        return await Task.FromResult(new ProgramResult(programs));
    }
}
