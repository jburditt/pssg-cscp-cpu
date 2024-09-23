namespace Manager;

public class InvoiceHandlers(IInvoiceRepository repository) : QueryBaseHandlers<IInvoiceRepository, Invoice, InvoiceQuery, InvoiceResult>(repository),
    IRequestHandler<InsertCommand<Invoice>, Guid>,
    IRequestHandler<InvoiceQuery, InvoiceResult>
{

}
