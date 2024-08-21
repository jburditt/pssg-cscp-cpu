using Manager.Contract;

namespace Resources;

public interface IPaymentRepository
{
    PaymentResult Query(PaymentQuery invoiceQuery);
}