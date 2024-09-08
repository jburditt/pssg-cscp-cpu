using Manager.Contract;

namespace Resources;

public interface IContractRepository
{
    Guid Insert(Contract contract);
    FindContractResult FirstOrDefault(FindContractQuery paymentQuery);
    ContractResult Query(ContractQuery paymentQuery);
}