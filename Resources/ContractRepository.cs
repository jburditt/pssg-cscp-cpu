using AutoMapper;
using Database.Model;
using Manager.Contract;

namespace Resources;

public class ContractRepository : BaseRepository, IContractRepository
{
    private readonly IMapper _mapper;

    public ContractRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext)
    {
        _mapper = mapper;
    }

    public Guid Insert(Contract contract)
    {
        var entity = _mapper.Map<Vsd_Contract>(contract);
        return base.Insert(entity);

    public Guid Upsert(Contract contract)
    {
        var entity = _mapper.Map<Vsd_Contract>(contract);
        return base.Upsert(entity);
    }

    public FindContractResult FirstOrDefault(FindContractQuery contractQuery)
    {
        var queryResults = _databaseContext.Vsd_ContractSet
            .WhereIf(contractQuery.Id != null, x => x.Id == contractQuery.Id)
            .FirstOrDefault();

        if (queryResults == null)
        {
            return new FindContractResult(null);
        }

        var contract = _mapper.Map<Contract>(queryResults);

        // if there is a customer, check if the customer is a reference to an account or contact, and load the corresponding method of payment from the referenced entity
        if (queryResults?.Vsd_Customer != null)
        {
            var customerEntityReferenceName = queryResults.Vsd_Customer.LogicalName;
            if (customerEntityReferenceName == Account.EntityLogicalName)
            {
                contract.MethodOfPayment = (MethodOfPayment?)_databaseContext.AccountSet.FirstOrDefault(p => p.Id == queryResults.Vsd_Customer.Id)?.Vsd_MethodOfPayment;
            }
            else if (customerEntityReferenceName == Contact.EntityLogicalName)
            {
                contract.MethodOfPayment = (MethodOfPayment?)_databaseContext.ContactSet.FirstOrDefault(p => p.Id == queryResults.Vsd_Customer.Id)?.Vsd_MethodOfPayment;
            }
        }

        return new FindContractResult(contract);
    }

    public ContractResult Query(ContractQuery contractQuery)
    {
        var query = _databaseContext.Vsd_ContractSet;

        if (contractQuery.Id != null)
        {
            query = query.Where(p => p.Id == contractQuery.Id);
        }

        var queryResults = query.ToList();
        var contract = _mapper.Map<IEnumerable<Contract>>(queryResults);
        return new ContractResult(contract);
    }

    public bool Delete(Guid id)
    {
        var entity = _databaseContext.Vsd_ContractSet.FirstOrDefault(x => x.Vsd_ContractId == id);
        if (entity == null)
        {
            return false;
        }
        return base.Delete(entity);
    }
}
