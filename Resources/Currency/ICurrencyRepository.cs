namespace Resources;

public interface ICurrencyRepository : IFindRepository<FindCurrencyQuery, FindCurrencyResult>, IQueryRepository<CurrencyQuery, CurrencyResult>, IBaseRepository<Currency>
{

}
