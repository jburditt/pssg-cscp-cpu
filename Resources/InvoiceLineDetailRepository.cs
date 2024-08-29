using AutoMapper;
using Database.Model;
using Manager.Contract;

namespace Resources
{
    public class InvoiceLineDetailRepository(DatabaseContext databaseContext, IMapper mapper) : IInvoiceLineDetailRepository
    {
        public Guid Insert(InvoiceLineDetail invoiceLineDetail)
        {
            var entity = mapper.Map<Vsd_InvoiceLineDetail>(invoiceLineDetail);
            databaseContext.AddObject(entity);
            databaseContext.SaveChanges();
            return entity.Id;
        }
    }
}
