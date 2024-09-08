namespace Manager.Contract;

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
    public StateCode StateCode { get; set; }
    public MethodOfPayment? MethodOfPayment { get; set; }
}
