using Manager.Contract;

namespace Resources;

public interface IContractRepository
{
    // CRUD
    Guid Insert(Contract contract);
    Guid Upsert(Contract contract);
    FindContractResult FirstOrDefault(FindContractQuery paymentQuery);
    ContractResult Query(ContractQuery paymentQuery);
    bool Delete(Guid id);

    // Custom queries
    bool IsCloned(Guid id);
}