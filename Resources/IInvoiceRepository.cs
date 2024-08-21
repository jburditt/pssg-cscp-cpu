using Manager.Contract;

namespace Resources;

public interface IInvoiceRepository
{
    InvoiceResult Query(InvoiceQuery invoiceQuery);
}