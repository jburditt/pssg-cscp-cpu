namespace Manager;

public class ProgramHandlers : QueryBaseHandlers<IProgramRepository, Program, ProgramQuery, ProgramResult>,
    IRequestHandler<ProgramQuery, ProgramResult>,
    IRequestHandler<GetApprovedCommand, ProgramResult>
{
    private readonly IMapper _mapper;

    public ProgramHandlers(IProgramRepository repository, IMapper mapper) : base(repository) 
    {
        _mapper = mapper;
    }

    public async Task<ProgramResult> Handle(GetApprovedCommand dummy, CancellationToken cancellationToken)
    {
        var programResults = _repository.GetApproved();
        var programs = _mapper.Map<IEnumerable<Program>>(programResults.Programs);
        return await Task.FromResult(new ProgramResult(programs));
    }
}
