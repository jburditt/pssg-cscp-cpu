using Manager.Contract;

namespace Resources;

public interface IInvoiceLineDetailRepository
{
    Guid Insert(InvoiceLineDetail invoiceLineDetail);
    IEnumerable<InvoiceLineDetail> Query(InvoiceLineDetailQuery query);
    bool Delete(Guid id);
}