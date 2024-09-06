using AutoMapper;
using Manager.Contract;
using Resources;

namespace Manager;

public class InvoiceLineDetailHandlers(IInvoiceLineDetailRepository invoiceLineDetailRepository) : 
    IRequestHandler<InvoiceLineDetail, Guid>,
    IRequestHandler<Guid, bool>
{
    public async Task<Guid> Handle(InvoiceLineDetail invoiceLineDetail, CancellationToken cancellationToken)
    {
        return invoiceLineDetailRepository.Insert(invoiceLineDetail);
    }

    public async Task<bool> Handle(Guid id, CancellationToken cancellationToken)
    {
        return invoiceLineDetailRepository.Delete(id);
    }
}
