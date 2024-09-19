namespace Resources;

public interface IProgramRepository
{
    Guid Upsert(Program program);
    ProgramResult Query(ProgramQuery programQuery);
    bool Delete(Guid id);
    ProgramResult GetApproved();
}
