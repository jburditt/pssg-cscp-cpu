using AutoMapper;
using Database.Model;
using Manager.Contract;

namespace Resources;

public class PaymentRepository(DatabaseContext databaseContext, IMapper mapper) : IPaymentRepository
{
    public PaymentResult Query(PaymentQuery paymentQuery)
    {
        var query = databaseContext.Vsd_PaymentSet
            .WhereIf(paymentQuery.ProgramId != null, p => p.Vsd_ProgramId.Id == paymentQuery.ProgramId)
            .WhereIf(paymentQuery.ContractId != null, p => p.Vsd_ContractId.Id == paymentQuery.ContractId);

        if (paymentQuery.ExcludeStatusCodes != null)
        {
            // TODO this could be a one liner 
            //query = query.Where(x => x.Payment.StatusCode == null || paymentQuery.ExcludeStatusCodes.Contains((PaymentStatusCode)x.Payment.StatusCode));
            //query = query.Where(p => p.StatusCode == null || !paymentQuery.ExcludeStatusCodes.Contains((PaymentStatusCode)p.StatusCode));
            foreach (var excludeStatusCode in paymentQuery.ExcludeStatusCodes)
            {
                query = query.Where(p => p.StatusCode != (Vsd_Payment_StatusCode)excludeStatusCode);
            }
        }

        var queryResults = query.ToList();
        var payments = mapper.Map<IEnumerable<Payment>>(queryResults);
        return new PaymentResult(payments);
    }
}
