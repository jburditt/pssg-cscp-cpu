using Manager.Contract;

namespace Resources;

public interface IContractRepository
{
    FindContractResult FirstOrDefault(FindContractQuery paymentQuery);
    ContractResult Query(ContractQuery paymentQuery);
}