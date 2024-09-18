using AutoMapper;
using Manager.Contract;
using Resources;

namespace Manager;

public class ContractHandlers(IContractRepository ContractRepository, IMapper mapper) : 
    IRequestHandler<ContractQuery, ContractResult>,
    IRequestHandler<FindContractQuery, FindContractResult>,
    IRequestHandler<Guid, bool>
{
    public async Task<FindContractResult> Handle(FindContractQuery ContractQuery, CancellationToken cancellationToken)
    {
        var contractResults = ContractRepository.FirstOrDefault(ContractQuery);
        return await Task.FromResult(new FindContractResult(contractResults.Contract));
    }

    public async Task<ContractResult> Handle(ContractQuery ContractQuery, CancellationToken cancellationToken)
    {
        var contractResults = ContractRepository.Query(ContractQuery);
        return await Task.FromResult(new ContractResult(contractResults.Contracts));
    }

    public async Task<bool> Handle(Guid id, CancellationToken cancellationToken)
    {
        var result = ContractRepository.IsCloned(id);
        return await Task.FromResult(result);
    }
}
