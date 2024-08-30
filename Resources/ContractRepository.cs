using AutoMapper;
using Database.Model;
using Manager.Contract;

namespace Resources;

public class ContractRepository(DatabaseContext databaseContext, IMapper mapper) : IContractRepository
{
    public FindContractResult FirstOrDefault(FindContractQuery contractQuery)
    {
        var queryResults = databaseContext.Vsd_ContractSet
            .WhereIf(contractQuery.Id != null, x => x.Id == contractQuery.Id)
            .FirstOrDefault();

        if (queryResults == null)
        {
            return new FindContractResult(null);
        }

        var contract = mapper.Map<Contract>(queryResults);

        // if there is a customer, check if the customer is a reference to an account or contact, and load the corresponding method of payment from the referenced entity
        if (queryResults?.Vsd_Customer != null)
        {
            var customerEntityReferenceName = queryResults.Vsd_Customer.LogicalName;
            if (customerEntityReferenceName == "account")
            {
                contract.MethodOfPayment = (MethodOfPayment?)databaseContext.AccountSet.FirstOrDefault(p => p.Id == queryResults.Vsd_Customer.Id)?.Vsd_MethodOfPayment;
            }
            else if (customerEntityReferenceName == "contact")
            {
                contract.MethodOfPayment = (MethodOfPayment?)databaseContext.ContactSet.FirstOrDefault(p => p.Id == queryResults.Vsd_Customer.Id)?.Vsd_MethodOfPayment;
            }
        }

        return new FindContractResult(contract);
    }

    public ContractResult Query(ContractQuery contractQuery)
    {
        var query = databaseContext.Vsd_ContractSet;

        if (contractQuery.Id != null)
        {
            query = query.Where(p => p.Id == contractQuery.Id);
        }

        var queryResults = query.ToList();
        var contract = mapper.Map<IEnumerable<Contract>>(queryResults);
        return new ContractResult(contract);
    }
}
