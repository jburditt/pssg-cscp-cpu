using AutoMapper;
using Database.Model;
using Manager.Contract;

namespace Resources;

public class InvoiceRepository(DatabaseContext databaseContext, IMapper mapper) : IInvoiceRepository
{
    public InvoiceResult Query(InvoiceQuery invoiceQuery)
    {
        var query = databaseContext.Vsd_InvoiceSet;

        if (invoiceQuery.ProgramId != null)
        {
            query = query.Where(c => c.Vsd_ProgramId.Id == invoiceQuery.ProgramId);
        }

        if (invoiceQuery.Origin != null) 
        {
            query = query.Where(c => c.Vsd_Origin == (Vsd_Invoice_Vsd_Origin)invoiceQuery.Origin);
        }

        if (invoiceQuery.InvoiceDate != null)
        {
            query = query.Where(i => i.Vsd_InvoicedAte == invoiceQuery.InvoiceDate);
        }

        var invoices = mapper.Map<IEnumerable<Invoice>>(query.ToList());
        return new InvoiceResult(invoices);
    }
}
