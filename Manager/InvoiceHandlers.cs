using AutoMapper;
using Manager.Contract;
using Resources;

namespace Manager;

public class InvoiceHandlers(IInvoiceRepository invoiceRepository, IMapper mapper) : IRequestHandler<InvoiceQuery, InvoiceResult>
{
    public async Task<Guid> Handle(Invoice invoice, CancellationToken cancellationToken = default)
    {
        return invoiceRepository.Insert(invoice);
    }

    public async Task<InvoiceResult> Handle(InvoiceQuery invoiceQuery, CancellationToken cancellationToken = default)
    {
        var invoiceResults = invoiceRepository.Query(invoiceQuery);
        return new InvoiceResult(invoiceResults.Invoices);
    }
}
