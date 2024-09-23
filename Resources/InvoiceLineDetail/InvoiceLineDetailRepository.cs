namespace Resources;

public class InvoiceLineDetailRepository : BaseRepository<Vsd_InvoiceLineDetail, InvoiceLineDetail>, IInvoiceLineDetailRepository
{
    public InvoiceLineDetailRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper) { }

    // TODO not completed and should return InvoiceLineDetailResult not IEnumerable
    public IEnumerable<InvoiceLineDetail> Query(InvoiceLineDetailQuery query)
    {
        var queryResults = _databaseContext.Vsd_InvoiceLineDetailSet
            .WhereIf(query.Id != null, x => x.Id == query.Id.Value)
            .ToList();
        return _mapper.Map<IEnumerable<InvoiceLineDetail>>(queryResults);
    }
}
