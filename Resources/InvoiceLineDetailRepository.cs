using AutoMapper;
using Database.Model;
using Manager.Contract;

namespace Resources
{
    public class InvoiceLineDetailRepository : BaseRepository, IInvoiceLineDetailRepository
    {
        private readonly IMapper _mapper;

        public InvoiceLineDetailRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext)
        {
            _mapper = mapper;
        }

        public Guid Insert(InvoiceLineDetail invoiceLineDetail)
        {
            var entity = _mapper.Map<Vsd_InvoiceLineDetail>(invoiceLineDetail);
            return base.Insert(entity);
        }

        public bool Delete(Guid id)
        {
            var entity = _databaseContext.Vsd_InvoiceLineDetailSet.Single(x => x.Id == id);
            return base.Delete(entity);
        }
    }
}
