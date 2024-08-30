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
        public bool Delete(Guid id)
        {
            var entity = databaseContext.Vsd_InvoiceLineDetailSet.Single(x => x.Id == id);
            databaseContext.DeleteObject(entity);
            databaseContext.SaveChanges();
            return true;
        }
    }
}
