using AutoMapper;
using Manager.Contract;
using Resources;

namespace Manager;

public class InvoiceLineDetailHandlers(IInvoiceLineDetailRepository invoiceLineDetailRepository) : IRequestHandler<InvoiceLineDetail, Guid>
{
    public async Task<Guid> Handle(InvoiceLineDetail invoiceLineDetail, CancellationToken cancellationToken = default)
    {
        return invoiceLineDetailRepository.Insert(invoiceLineDetail);
    }
}
