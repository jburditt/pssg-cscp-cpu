namespace Resources;

public class PaymentRepository : BaseRepository<Vsd_Payment, Payment>, IPaymentRepository
{
    public PaymentRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper) { }

    public PaymentResult Query(PaymentQuery paymentQuery)
    {
        var query = _databaseContext.Vsd_PaymentSet
            .WhereIf(paymentQuery.ProgramId != null, p => p.Vsd_ProgramId.Id == paymentQuery.ProgramId)
            .WhereIf(paymentQuery.ContractId != null, p => p.Vsd_ContractId.Id == paymentQuery.ContractId);
            //.ExcludesIf(paymentQuery.ExcludeStatusCodes != null, p => p.StatusCode, paymentQuery.ExcludeStatusCodes);

        if (paymentQuery.ExcludeStatusCodes != null)
        {
            // TODO this could be a one liner, Linq Contains and BinarySearch do not translate from IQueryable to Dynamics SQL and I wasn't smart enough to figure out 'ExcludesIf'
            // TODO try the DataverseExtensions WhereNotIn
            foreach (var excludeStatusCode in paymentQuery.ExcludeStatusCodes)
            {
                query = query.Where(p => p.StatusCode != (Vsd_Payment_StatusCode)excludeStatusCode);
            }
        }

        var queryResults = query.ToList();
        var payments = _mapper.Map<IEnumerable<Payment>>(queryResults);
        return new PaymentResult(payments);
    }
}
