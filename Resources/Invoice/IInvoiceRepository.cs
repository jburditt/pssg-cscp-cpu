namespace Resources;

public interface IInvoiceRepository : IQueryRepository<InvoiceQuery, InvoiceResult>, IBaseRepository<Invoice>
{

}