using AutoMapper;
using Database.Model;
using Manager.Contract;

namespace Resources;

public class ContractRepository(DatabaseContext databaseContext, IMapper mapper) : IContractRepository
{
    public FindContractResult FirstOrDefault(FindContractQuery contractQuery)
    {
        var query = databaseContext.Vsd_ContractSet;

        if (contractQuery.Id != null)
        {
            query = query.Where(p => p.Id == contractQuery.Id);
        }

        var queryResults = query.FirstOrDefault();
        var contract = mapper.Map<Contract>(queryResults);
        return new FindContractResult(contract);
    }

    public ContractResult Query(ContractQuery contractQuery)
    {
        var query = databaseContext.Vsd_ContractSet;

        if (contractQuery.Id != null)
        {
            query = query.Where(p => p.Id == contractQuery.Id);
        }

        var queryResults = query.FirstOrDefault();
        var contract = mapper.Map<IEnumerable<Contract>>(queryResults);
        return new ContractResult(contract);
    }
}
