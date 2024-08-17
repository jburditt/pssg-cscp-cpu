using Manager.Contract;

namespace Resources;

public interface IProgramRepository
{
    ProgramResult Query(ProgramQuery programQuery);
    ProgramResult GetApproved();
}

//public record ProgramQuery
//{
//    public StateCode? StateCode { get; set; }
//    //public StatusCode StatusCode { get; set; }
//}

//public record ProgramResult(IEnumerable<Program> Programs);

//public record Program(StateCode StateCode, StatusCode StatusCode);