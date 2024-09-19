namespace Resources;

public class ContractRepository : BaseRepository, IContractRepository
{
    private readonly IMapper _mapper;

    public ContractRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext)
    {
        _mapper = mapper;
    }

    #region CRUD

    public Guid Insert(Contract contract)
    {
        var entity = _mapper.Map<Vsd_Contract>(contract);
        return base.Insert(entity);
    }

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
        var queryResults = _databaseContext.Vsd_ContractSet
            .WhereIf(contractQuery.Id != null, x => x.Id == contractQuery.Id)
            .WhereIf(contractQuery.StateCode != null, x => x.StateCode == (Vsd_Contract_StateCode?)contractQuery.StateCode)
            .WhereIf(contractQuery.StatusCode != null, x => x.StatusCode == (Vsd_Contract_StatusCode?)contractQuery.StatusCode)
            .WhereIf(contractQuery.CpuCloneFlag != null, x => x.Vsd_CpuCloneFlag == contractQuery.CpuCloneFlag)
            .ToList();
        var contract = _mapper.Map<IEnumerable<Contract>>(queryResults);
        return new ContractResult(contract);
    }

    // Safe Delete, will not throw exception if entity does not exist, but comes at cost of performance
    public bool Delete(Guid id)
    {
        var entity = _databaseContext.Vsd_ContractSet.FirstOrDefault(x => x.Vsd_ContractId == id);
        if (entity == null)
        {
            return false;
        }
        return base.Delete(entity);
    }

    #endregion CRUD

    public bool IsCloned(Guid id)
    {
        var entity = _databaseContext.Vsd_ContractSet.FirstOrDefault(x => x.Vsd_ClonedContractId.Id == id);
        return entity != null;
    }

    public Guid? Clone(Contract contract)
    {
        var entity = _mapper.Map<Vsd_Contract>(contract);
        var request = new Vsd_CloneContractRequest();
        request.Target = entity.ToEntityReference();
        var response = (Vsd_CloneContractResponse)_databaseContext.Execute(request);
        if (response.IsSuccess)
        {
            return new Guid(response.Result);
        }
        else
        {
            return null;
        }
    }
}
