namespace Manager;

public class ContractHandlers : 
    FindQueryBaseHandlers<IContractRepository, Contract.Contract, FindContractQuery, FindContractResult, ContractQuery, ContractResult>,
    IRequestHandler<ContractQuery, ContractResult>,
    IRequestHandler<FindContractQuery, FindContractResult>,
    IRequestHandler<Guid, bool>,
    IRequestHandler<CloneCommand, Guid?>
{
    public ContractHandlers(IContractRepository repository) : base(repository) { }

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
