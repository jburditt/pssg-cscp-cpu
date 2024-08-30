using Manager.Contract;

namespace Resources;

public interface IProgramRepository
{
    ProgramResult Query(ProgramQuery programQuery);
    ProgramResult GetApproved();
}
