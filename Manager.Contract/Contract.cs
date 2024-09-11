namespace Manager.Contract;

public enum ContractStatusCode
{
    Archived = 2,
    Cancelled = 100000007,
    Draft = 1,
    DulyExecuted = 100000006,
    Escalated = 100000004,
    InformationDenied = 100000005,
    Processing = 100000002,
    Received = 100000001,
    Sent = 100000000,
    UnderReview = 100000008,
}

public enum ContractType
{
    GeneralServiceAgreement = 100000006,
    MemorandumOfUnderstanding = 100000007,
    TuaCommunityAccountabilityPrograms = 100000002,
    TuaContinuingAgreement = 100000001,
    TuaCrimePrevention = 100000003,
    TuaCustomServices = 100000005,
    TuaProvincialAssociation = 100000004,
    TuaVictimServicesVawp = 100000000,
}

public record FindContractQuery : IRequest<FindContractResult>
{
    public Guid? Id { get; set; }
}

public record FindContractResult(Contract? Contract);

public record ContractQuery : IRequest<ContractResult>
{
    public Guid? Id { get; set; }
}

public record ContractResult(IEnumerable<Contract> Contracts);

public record Contract
{
    public Guid Id { get; set; }
    public StateCode? StateCode { get; set; }
    public ContractStatusCode? StatusCode { get; set; }
    public MethodOfPayment? MethodOfPayment { get; set; }
    public ContractType? ContractType { get; set; }
    // References
    public Guid? ProgramId { get; set; }
}
