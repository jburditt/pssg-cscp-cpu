using AutoMapper;
using Database.Model;
using Manager.Contract;

namespace Resources;

public class PaymentRepository(DatabaseContext databaseContext, IMapper mapper) : IPaymentRepository
{
    public PaymentResult Query(PaymentQuery paymentQuery)
    {
        //var query = 
        //    from payment in databaseContext.Vsd_PaymentSet
        //    join program in databaseContext.Vsd_ProgramSet on payment.Vsd_ProgramId.Id equals program.Vsd_ProgramId
        //    join contract in databaseContext.Vsd_ContractSet on payment.Vsd_ContractId.Id equals contract.Vsd_ContractId
        //    //where paymentQuery.ProgramId == null || program.Id == paymentQuery.ProgramId
        //    //where paymentQuery.ContractId == null || contract.Id == paymentQuery.ContractId
        //    //where paymentQuery.ExcludeStatusCodes == null || (payment.StatusCode == null || !paymentQuery.ExcludeStatusCodes.Contains((PaymentStatusCode)payment.StatusCode))
        //    select new { Payment = payment, Program = program, Contract = contract };
        var query = databaseContext.Vsd_PaymentSet;

        if (paymentQuery.ProgramId != null)
        {
        //    query = from payment in query.
        //    query = query.Where(c => c.Program.Id == paymentQuery.ProgramId);
            query = query.Where(p => p.Vsd_ProgramId.Id == paymentQuery.ProgramId);
        }

        if (paymentQuery.ContractId != null)
        {
            //    query = query.Where(c => c.Contract.Id == paymentQuery.ContractId);
            query = query.Where(p => p.Vsd_ContractId.Id == paymentQuery.ContractId);
        }

        if (paymentQuery.ExcludeStatusCodes != null)
        {
            //    query = query.Where(x => x.Payment.StatusCode == null || paymentQuery.ExcludeStatusCodes.Contains((PaymentStatusCode)x.Payment.StatusCode));
            foreach (var excludeStatusCode in paymentQuery.ExcludeStatusCodes)
            {
                query = query.Where(p => p.StatusCode != (Vsd_Payment_StatusCode)excludeStatusCode);
            }
            //query = query.Where(p => p.StatusCode == null || !paymentQuery.ExcludeStatusCodes.Contains((PaymentStatusCode)p.StatusCode));
        }

        var queryResults = query.ToList();//.Select(x => x.Payment);
        var payments = mapper.Map<IEnumerable<Payment>>(queryResults);
        return new PaymentResult(payments);
    }
}
