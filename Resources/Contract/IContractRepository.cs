namespace Resources;

public interface IContractRepository : IBaseRepository<Contract>, IFindRepository<FindContractQuery, FindContractResult>, IQueryRepository<ContractQuery, ContractResult>
{
    // Custom queries
    bool IsCloned(Guid id);
    Guid? Clone(Contract contract);
}