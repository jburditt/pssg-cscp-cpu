using AutoMapper;
using Manager.Contract;
using Resources;

namespace Manager;

public class PaymentHandlers(IPaymentRepository paymentRepository, IMapper mapper) : IRequestHandler<PaymentQuery, PaymentResult>
{
    public async Task<PaymentResult> Handle(PaymentQuery paymentQuery, CancellationToken cancellationToken)
    {
        var paymentResults = paymentRepository.Query(paymentQuery);
        return await Task.FromResult(new PaymentResult(paymentResults.Payments));
    }
}
