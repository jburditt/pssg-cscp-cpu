namespace Manager;

public class ContractHandlers : 
    FindQueryBaseHandlers<IContractRepository, Contract.Contract, FindContractQuery, FindContractResult, ContractQuery, ContractResult>,
    IRequestHandler<ContractQuery, ContractResult>,
    IRequestHandler<FindContractQuery, FindContractResult>,
    IRequestHandler<Guid, bool>,
    IRequestHandler<CloneCommand, Guid?>
{
    public ContractHandlers(IContractRepository repository) : base(repository) { }

    // FirstOrDefault
    public async Task<FindContractResult> Handle(FindContractQuery ContractQuery, CancellationToken cancellationToken)
    {
        var contractResults = ContractRepository.FirstOrDefault(ContractQuery);
        return await Task.FromResult(new FindContractResult(contractResults.Contract));
    }

    // Query
    public async Task<ContractResult> Handle(ContractQuery ContractQuery, CancellationToken cancellationToken)
    {
        var contractResults = ContractRepository.Query(ContractQuery);
        return await Task.FromResult(new ContractResult(contractResults.Contracts));
    }

    // IsCloned
    public async Task<bool> Handle(Guid id, CancellationToken cancellationToken)
    {
        var result = _repository.IsCloned(id);
        return await Task.FromResult(result);
    }

    // Clone
    public async Task<Guid?> Handle(CloneCommand cloneCommand, CancellationToken cancellationToken)
    {
        var result = _repository.Clone(cloneCommand.Contract);
        return await Task.FromResult(result);
    }
}
