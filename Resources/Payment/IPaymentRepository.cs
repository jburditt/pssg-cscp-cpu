namespace Resources;

public interface IPaymentRepository : IQueryRepository<PaymentQuery, PaymentResult>, IBaseRepository<Payment>
{

}