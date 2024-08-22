using AutoMapper;
using Manager.Contract;
using Resources;

namespace Manager;

public class PaymentHandlers(IPaymentRepository paymentRepository, IMapper mapper) : IRequestHandler<PaymentQuery, PaymentResult>
{
    public async Task<PaymentResult> Handle(PaymentQuery paymentQuery, CancellationToken cancellationToken = default)
    {
        var paymentResults = paymentRepository.Query(paymentQuery);
        //var paymentResults = mapper.Map<IEnumerable<Payment>>(currencies.Currencies);
        return new PaymentResult(paymentResults.Payments);
    }
}
