namespace Manager.Contract;

// TODO throughout this solution there are Dynamics values used outside of the "Database" project. The problem with this is if you ever changed database, these values would 
// no longer make sense. It would be better to not use Dynamics specific values. The values are provided currently for ease of mapping entities to dto and vice-versa.
// A minor improvement is to use the enum field name, but then the field names are still potentially Dynamics specific. The best solution is to manually map these values
// and figure out a way to have build-time compilation errors on newly added enum fields that are not mapped
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
    public StateCode? StateCode { get; set; }
    public ContractStatusCode? StatusCode { get; set; }
    public bool? CpuCloneFlag { get; set; }
    public bool? NotNullCustomer { get; set; }
    public bool? NotNullFiscalStartDate { get; set; }
    public bool? NotNullFiscalEndDate { get; set; }
    public ContractType? NotEqualType { get; set; }
}

public record ContractResult(IEnumerable<Contract> Contracts);

public record Contract : IDto
{
    public Guid Id { get; set; }
    public StateCode StateCode { get; set; }
    public ContractStatusCode? StatusCode { get; set; }

    public MethodOfPayment? MethodOfPayment { get; set; }
    public ContractType? ContractType { get; set; }

    // References
    public Guid? ProgramId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? ClonedContractId { get; set; }
}

public record IsClonedCommand(Guid Id) : IRequest<bool>;

public record CloneCommand(Contract Contract) : IRequest<Guid?>;
