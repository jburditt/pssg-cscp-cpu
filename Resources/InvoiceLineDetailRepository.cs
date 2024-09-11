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

        // TODO not completed and should return InvoiceLineDetailResult not IEnumerable
        public IEnumerable<InvoiceLineDetail> Query(InvoiceLineDetailQuery query)
        {
            var queryResults = _databaseContext.Vsd_InvoiceLineDetailSet
                .WhereIf(query.Id != null, x => x.Id == query.Id.Value)
                .ToList();
            return _mapper.Map<IEnumerable<InvoiceLineDetail>>(queryResults);
        }

        public bool Delete(Guid id)
        {
            var entity = _databaseContext.Vsd_InvoiceLineDetailSet.Single(x => x.Id == id);
            return base.Delete(entity);
        }
    }
}
