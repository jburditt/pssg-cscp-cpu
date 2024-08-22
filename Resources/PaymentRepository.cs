using AutoMapper;
using Database.Model;
using Manager.Contract;

namespace Resources;

public class PaymentRepository(DatabaseContext databaseContext, IMapper mapper) : IPaymentRepository
{
    public PaymentResult Query(PaymentQuery paymentQuery)
    {
        var query = 
            from payment in databaseContext.Vsd_PaymentSet
            join program in databaseContext.Vsd_ProgramSet on payment.Vsd_ProgramId.Id equals program.Vsd_ProgramId
            join contract in databaseContext.Vsd_ContractSet on payment.Vsd_ContractId.Id equals contract.Vsd_ContractId
            select new { Payment = payment, Program = program, Contract = contract };

        //if (paymentQuery.ProgramId != null)
        //{
        //    query = query.Where(c => c.Program.Id == paymentQuery.ProgramId);
        //}

        //if (paymentQuery.ContractId != null)
        //{
        //    query = query.Where(c => c.Contract.Id == paymentQuery.ContractId);
        //}

        //if (paymentQuery.ExcludeStatusCodes != null)
        //{
        //    query = query.Where(x => x.Payment.StatusCode == null || paymentQuery.ExcludeStatusCodes.Contains((PaymentStatusCode)x.Payment.StatusCode));
        //}

        var queryResults = query.ToList().Select(x => x.Payment);
        var payments = mapper.Map<IEnumerable<Payment>>(queryResults);
        return new PaymentResult(payments);
    }
}
