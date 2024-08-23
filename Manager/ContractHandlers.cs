using AutoMapper;
using Manager.Contract;
using Resources;

namespace Manager;

public class ContractHandlers(IContractRepository ContractRepository, IMapper mapper) : 
    IRequestHandler<ContractQuery, ContractResult>,
    IRequestHandler<FindContractQuery, FindContractResult>
{
    public async Task<FindContractResult> Handle(FindContractQuery ContractQuery, CancellationToken cancellationToken = default)
    {
        var contractResults = ContractRepository.FirstOrDefault(ContractQuery);
        return await Task.FromResult(new FindContractResult(contractResults.Contract));
    }

    public async Task<ContractResult> Handle(ContractQuery ContractQuery, CancellationToken cancellationToken = default)
    {
        var contractResults = ContractRepository.Query(ContractQuery);
        return await Task.FromResult(new ContractResult(contractResults.Contracts));
    }
}
