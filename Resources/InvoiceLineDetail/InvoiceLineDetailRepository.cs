namespace Resources;

public class InvoiceLineDetailRepository : BaseRepository<Vsd_InvoiceLineDetail, InvoiceLineDetail>, IInvoiceLineDetailRepository
{
    public InvoiceLineDetailRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper) { }
}
