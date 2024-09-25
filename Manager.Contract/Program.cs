namespace Manager.Contract;

public enum ProgramStatusCode
{
    ApplicationInfoReceived = 100000001,
    ApplicationInfoSent = 100000000,
    Archived = 2,
    BudgetProposalDraft = 100000002,
    BudgetProposalReceived = 100000006,
    BudgetProposalSent = 100000003,
    Cancelled = 100000007,
    Completed = 100000008,
    Draft = 1,
    Escalated = 100000004,
    InformationDenied = 100000005,
    ProcessingBudgetProposal = 100000009,
}

public record ProgramQuery : IRequest<ProgramResult>
{
    public Guid? Id { get; set; }
    public StateCode? StateCode { get; set; }
    public ProgramStatusCode? StatusCode { get; set; }
    public Guid? ContractId { get; set; }
}

public record ProgramResult(IEnumerable<Program> Programs);

public record Program : IDto
{
    public Guid Id { get; set; }
    public StateCode StateCode { get; set; }
    public ProgramStatusCode? StatusCode { get; set; }
    public string? Name { get; set; }
    public string? ProvinceState { get; set; }  // business recommended but not required in Dynamics
    public DateTime? BudgetProposalSignatureDate { get; set; }
    public decimal? CpuSubtotal { get; set; }

    // References
    public Guid? ContractId { get; set; }
    public Guid? OwnerId { get; set; }

    // Columns from other tables
    public string? ContractName { get; set; }
}

public class GetApprovedCommand() : IRequest<ProgramResult>;
