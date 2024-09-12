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
        return await Task.FromResult(invoiceLineDetailRepository.Insert(invoiceLineDetail));
    }

    public async Task<bool> Handle(Guid id, CancellationToken cancellationToken)
    {
        return await Task.FromResult(invoiceLineDetailRepository.Delete(id));
    }
}
