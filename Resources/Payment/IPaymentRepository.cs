namespace Resources;

public interface IPaymentRepository
{
    PaymentResult Query(PaymentQuery paymentQuery);
}