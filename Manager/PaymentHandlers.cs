namespace Manager;

public class PaymentHandlers(IPaymentRepository repository) : QueryBaseHandlers<IPaymentRepository, Payment, PaymentQuery, PaymentResult>(repository),
    IRequestHandler<PaymentQuery, PaymentResult>
{

}
