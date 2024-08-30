using AutoMapper;
using Database.Model;
using Manager.Contract;

namespace Resources;

public class InvoiceRepository : BaseRepository, IInvoiceRepository
{
    private readonly IMapper _mapper;

    public InvoiceRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext)
    {
        _mapper = mapper;
    }

    public Guid Insert(Invoice invoice)
    {
        var entity = _mapper.Map<Vsd_Invoice>(invoice);
        return base.Insert(entity);
    }

    public InvoiceResult Query(InvoiceQuery invoiceQuery)
    {
        var query = _databaseContext.Vsd_InvoiceSet;

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
        var invoices = _mapper.Map<IEnumerable<Invoice>>(queryResults);
        return new InvoiceResult(invoices);
    }
}
