namespace Manager;

public class InvoiceLineDetailHandlers(IInvoiceLineDetailRepository repository) : BaseHandlers<IInvoiceLineDetailRepository, InvoiceLineDetail>(repository),
    IRequestHandler<InsertCommand<InvoiceLineDetail>, Guid>
{

}
