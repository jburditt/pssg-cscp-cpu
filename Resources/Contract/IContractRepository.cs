namespace Resources;

public interface IContractRepository : IBaseRepository<Contract>, IFindRepository<FindContractQuery, FindContractResult>, IQueryRepository<ContractQuery, ContractResult>
{
    FindContractResult FirstOrDefault(FindContractQuery paymentQuery);

    // Custom queries
    bool IsCloned(Guid id);
    Guid? Clone(Contract contract);
}