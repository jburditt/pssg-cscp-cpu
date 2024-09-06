using Manager.Contract;
using Resources;

namespace Manager;

public class InvoiceHandlers(IInvoiceRepository invoiceRepository) : 
    IRequestHandler<Invoice, Guid>,
    IRequestHandler<InvoiceQuery, InvoiceResult>
{
    public async Task<Guid> Handle(Invoice invoice, CancellationToken cancellationToken)
    {
        return invoiceRepository.Insert(invoice);
    }

    public async Task<InvoiceResult> Handle(InvoiceQuery invoiceQuery, CancellationToken cancellationToken)
    {
        var invoiceResults = invoiceRepository.Query(invoiceQuery);
        return new InvoiceResult(invoiceResults.Invoices);
    }
}
