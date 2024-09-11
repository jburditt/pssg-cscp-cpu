using Manager.Contract;

namespace Resources;

public interface IContractRepository
{
    Guid Insert(Contract contract);
    Guid Upsert(Contract contract);
    FindContractResult FirstOrDefault(FindContractQuery paymentQuery);
    ContractResult Query(ContractQuery paymentQuery);
    bool Delete(Guid id);
}