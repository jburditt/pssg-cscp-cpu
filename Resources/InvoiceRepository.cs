using AutoMapper;
using Database.Model;
using Manager.Contract;

namespace Resources;

public class InvoiceRepository(DatabaseContext databaseContext, IMapper mapper) : IInvoiceRepository
{
    public Guid Insert(Invoice invoice)
    {
        var entity = mapper.Map<Vsd_Invoice>(invoice);
        databaseContext.AddObject(entity);
        databaseContext.SaveChanges();
        return entity.Id;
    }

    public InvoiceResult Query(InvoiceQuery invoiceQuery)
    {
        var query = databaseContext.Vsd_InvoiceSet;
            //from invoice in databaseContext.Vsd_InvoiceSet
            //join program in databaseContext.Vsd_ProgramSet on invoice.Vsd_ProgramId.Id equals program.Vsd_ProgramId
            //select new { Invoice = invoice, Program = program };

        if (invoiceQuery.ProgramId != null)
        {
            // TODO test this works as expected
            query = query.Where(c => c.Vsd_Vsd_Program_Vsd_Invoice.Id == invoiceQuery.ProgramId);
        }

        if (invoiceQuery.Origin != null)
        {
            query = query.Where(c => c.Vsd_Origin == (Vsd_Invoice_Vsd_Origin)invoiceQuery.Origin);
        }

        if (invoiceQuery.InvoiceDate != null)
        {
            query = query.Where(i => i.Vsd_InvoicedAte == invoiceQuery.InvoiceDate);
        }

        var queryResults = query.ToList();
        var invoices = mapper.Map<IEnumerable<Invoice>>(queryResults);
        return new InvoiceResult(invoices);
    }
}
