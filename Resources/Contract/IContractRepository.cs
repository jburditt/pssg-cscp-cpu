namespace Resources;

public interface IContractRepository : IBaseRepository<Contract>, IFindRepository<FindContractQuery, FindContractResult>, IQueryRepository<ContractQuery, ContractResult>
{
    bool IsCloned(Guid id);
    Guid? Clone(Contract contract);
}