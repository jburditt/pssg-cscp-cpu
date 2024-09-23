namespace Resources;

public interface IProgramRepository : IBaseRepository<Program>, IQueryRepository<ProgramQuery, ProgramResult>
{
    ProgramResult GetApproved();
}
