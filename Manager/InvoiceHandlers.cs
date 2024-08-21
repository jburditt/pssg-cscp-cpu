using AutoMapper;
using Manager.Contract;
using Resources;

namespace Manager;

public class InvoiceHandlers(IInvoiceRepository invoiceRepository, IMapper mapper) : IRequestHandler<InvoiceQuery, InvoiceResult>
{
    public async Task<InvoiceResult> Handle(InvoiceQuery invoiceQuery, CancellationToken cancellationToken = default)
    {
        var invoiceResults = invoiceRepository.Query(invoiceQuery);
        //var invoiceResults = mapper.Map<IEnumerable<Invoice>>(invoices.Invoices);
        return new InvoiceResult(invoiceResults.Invoices);
    }
}
