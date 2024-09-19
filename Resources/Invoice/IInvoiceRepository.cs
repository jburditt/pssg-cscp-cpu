namespace Resources;

public interface IInvoiceRepository
{
    Guid Insert(Invoice invoice);
    InvoiceResult Query(InvoiceQuery invoiceQuery);
}