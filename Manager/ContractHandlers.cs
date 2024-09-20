namespace Manager;

public class ContractHandlers(IContractRepository ContractRepository, IMapper mapper) : 
    IRequestHandler<ContractQuery, ContractResult>,
    IRequestHandler<FindContractQuery, FindContractResult>,
    IRequestHandler<Guid, bool>,
    IRequestHandler<Contract.Contract, Guid?>
{
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
        var result = ContractRepository.IsCloned(id);
        return await Task.FromResult(result);
    }

    // Clone
    public async Task<Guid?> Handle(Contract.Contract contract, CancellationToken cancellationToken)
    {
        var result = ContractRepository.Clone(contract);
        return await Task.FromResult(result);
    }
}
